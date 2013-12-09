namespace Proligence.Orchard.Testing
{
    using System;
    using System.Reflection;
    using Moq;
    using Proligence.Orchard.Testing.Mocks;
    using global::Orchard.ContentManagement;
    using global::Orchard.ContentManagement.Drivers;
    using global::Orchard.ContentManagement.Handlers;
    using global::Orchard.DisplayManagement;

    public static class ContentShapeResultExtensions
    {
        public static ShapeMock BuildShapeMock(this ContentShapeResult shapeResult)
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo shapeBuilderField = typeof(ContentShapeResult).GetField("_shapeBuilder", bindingFlags);
            var builder = (Func<BuildShapeContext, dynamic>)shapeBuilderField.GetValue(shapeResult);

            var content = new Mock<IContent>();
            content.Setup(x => x.ContentItem).Returns(new ContentItem { ContentType = "Mock" });

            var shape = new Mock<IShape>();
            var shapeFactoryMock = new ShapeFactoryMock();
            var ctx = new Mock<BuildShapeContext>(shape.Object, content.Object, string.Empty, shapeFactoryMock).Object;

            var result = (ShapeMock)builder(ctx);

            if (result != null)
            {
                result.Type = shapeResult.GetShapeType();
            }

            return result;
        }
    }
}