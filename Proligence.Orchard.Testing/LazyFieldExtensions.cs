namespace Proligence.Orchard.Testing
{
    using System.Reflection;
    using global::Orchard.Core.Common.Utilities;

    public static class LazyFieldExtensions
    {
        public static bool HasLoader<T>(this LazyField<T> lazyField)
        {
            return ReadField(lazyField, "_loader") != null;
        }

        public static bool HasSetter<T>(this LazyField<T> lazyField)
        {
            return ReadField(lazyField, "_setter") != null;
        }

        public static bool HasValue<T>(this LazyField<T> lazyField)
        {
            return ReadField(lazyField, "_value") != null;
        }

        private static object ReadField<T>(LazyField<T> lazyField, string name)
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            return lazyField.GetType().GetField(name, bindingFlags).GetValue(lazyField);
        }
    }
}