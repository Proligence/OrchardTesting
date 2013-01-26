namespace Proligence.Orchard.Testing.Mocks
{
    using System.Collections.Generic;
    using global::Orchard.ContentManagement;
    using global::Orchard.DisplayManagement;
    using global::Orchard.DisplayManagement.Shapes;

    public class ShapeMock : IShape
    {
        public ShapeMock(string shapeType)
        {
            Type = shapeType;
            Data = new Dictionary<string, object>();
            Metadata = new ShapeMetadata { Type = shapeType };
        }

        public string Type { get; private set; }
        public ShapeMetadata Metadata { get; set; }
        public ContentItem ContentItem { get; set; }
        internal Dictionary<string, object> Data { get; private set; }

        public object this[string name]
        {
            get
            {
                return Data[name];
            }
        }
    }
}