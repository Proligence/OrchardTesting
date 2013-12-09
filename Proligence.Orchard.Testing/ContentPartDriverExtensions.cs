namespace Proligence.Orchard.Testing
{
    using System.Reflection;
    using global::Orchard.ContentManagement;
    using global::Orchard.ContentManagement.Drivers;

    public static class ContentPartDriverExtensions
    {
        public static DriverResult InvokeDisplay<TPart>(
            this ContentPartDriver<TPart> driver, TPart part, string displayType, dynamic shapeHelper)
            where TPart : ContentPart, new()
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            var argumentTypes = new[] { typeof(TPart), typeof(string), typeof(object) };
            return (DriverResult)driver
                .GetType()
                .GetMethod("Display", bindingFlags, null, argumentTypes, null)
                .Invoke(driver, new object[] { part, displayType, shapeHelper });
        }

        public static DriverResult InvokeEditor<TPart>(
            this ContentPartDriver<TPart> driver, TPart part, dynamic shapeHelper)
            where TPart : ContentPart, new()
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            var argumentTypes = new[] { typeof(TPart), typeof(object) };
            return (DriverResult)driver
                .GetType()
                .GetMethod("Editor", bindingFlags, null, argumentTypes, null)
                .Invoke(driver, new object[] { part, shapeHelper });
        }

        public static DriverResult InvokeEditor<TPart>(
            this ContentPartDriver<TPart> driver, TPart part, IUpdateModel updater, dynamic shapeHelper)
            where TPart : ContentPart, new()
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            var argumentTypes = new[] { typeof(TPart), typeof(IUpdateModel), typeof(object) };
            return (DriverResult)driver
                .GetType()
                .GetMethod("Editor", bindingFlags, null, argumentTypes, null)
                .Invoke(driver, new object[] { part, updater, shapeHelper });
        }
    }
}