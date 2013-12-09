namespace Proligence.Orchard.Testing
{
    using System.Linq;
    using Proligence.Orchard.Testing.Mocks;
    using global::Orchard.ContentManagement.Drivers;

    public static class CombinedResultExtensions
    {
        public static ShapeMock BuildShapeMock(this CombinedResult combinedResult, int index)
        {
            DriverResult[] driverResults = combinedResult.GetResults().ToArray();
            if (driverResults.Length > index)
            {
                var shapeResult = driverResults[index] as ContentShapeResult;
                if (shapeResult != null)
                {
                    return shapeResult.BuildShapeMock();
                }
            }

            return null;
        }

        public static ShapeMock BuildShapeMock(this CombinedResult combinedResult, string shapeType)
        {
            DriverResult[] driverResults = combinedResult.GetResults().ToArray();
            foreach (DriverResult driverResult in driverResults)
            {
                var shapeResult = driverResult as ContentShapeResult;
                if ((shapeResult != null) && (shapeResult.GetShapeType() == shapeType))
                {
                    return shapeResult.BuildShapeMock();
                }
            }
            
            return null;
        }
    }
}