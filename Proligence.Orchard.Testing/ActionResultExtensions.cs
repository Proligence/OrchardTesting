namespace Proligence.Orchard.Testing
{
    using System.Web.Mvc;
    using global::Orchard.Mvc;
    using Proligence.Orchard.Testing.Mocks;

    public static class ActionResultExtensions
    {
        public static ShapeMock AsShape(this ActionResult result)
        {
            if (result != null)
            {
                var shapeResult = result as ShapeResult;
                if (shapeResult != null)
                {
                    return (ShapeMock)shapeResult.Model;
                }
            }

            return null;
        }
    }
}