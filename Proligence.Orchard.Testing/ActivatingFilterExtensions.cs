namespace Proligence.Orchard.Testing
{
    using System;
    using System.Reflection;
    using global::Orchard.ContentManagement;
    using global::Orchard.ContentManagement.Handlers;

    public static class ActivatingFilterExtensions
    {
        public static Func<string, bool> GetPredicate<T>(this ActivatingFilter<T> filter)
            where T : ContentPart, new()
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo predicateField = filter.GetType().GetField("_predicate", bindingFlags);

            return (Func<string, bool>)predicateField.GetValue(filter);
        }
    }
}