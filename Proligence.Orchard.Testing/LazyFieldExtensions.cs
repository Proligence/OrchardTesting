namespace Proligence.Orchard.Testing
{
    using System;
    using System.Reflection;
    using global::Orchard.Core.Common.Utilities;

    public static class LazyFieldExtensions
    {
        private const string LoaderFieldName = "_loader";
        private const string SetterFieldName = "_setter";
        private const string ValueFieldName = "_value";

        public static bool HasLoader<T>(this LazyField<T> lazyField)
        {
            return ReadField(lazyField, LoaderFieldName) != null;
        }

        public static Func<T> GetLoader<T>(this LazyField<T> lazyField)
        {
            return (Func<T>)ReadField(lazyField, LoaderFieldName);
        }

        public static bool HasSetter<T>(this LazyField<T> lazyField)
        {
            return ReadField(lazyField, SetterFieldName) != null;
        }

        public static Func<T, T> GetSetter<T>(this LazyField<T> lazyField)
        {
            return (Func<T, T>)ReadField(lazyField, SetterFieldName);
        }

        public static bool HasValue<T>(this LazyField<T> lazyField)
        {
            return ReadField(lazyField, ValueFieldName) != null;
        }

        public static T GetValue<T>(this LazyField<T> lazyField)
        {
            return (T)ReadField(lazyField, ValueFieldName);
        }

        private static object ReadField<T>(LazyField<T> lazyField, string name)
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            return lazyField.GetType().GetField(name, bindingFlags).GetValue(lazyField);
        }
    }
}