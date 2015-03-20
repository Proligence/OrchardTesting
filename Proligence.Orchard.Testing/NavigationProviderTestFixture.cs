namespace Proligence.Orchard.Testing
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
#if (NUNIT)
    using NUnit.Framework;
#endif
    using global::Orchard.Localization;
    using global::Orchard.Security.Permissions;
    using global::Orchard.UI.Navigation;

    public abstract class NavigationProviderTestFixture<TProvider>
        where TProvider : INavigationProvider, new()
    {
#if (XUNIT)
        protected NavigationProviderTestFixture()
        {
            Provider = new TProvider();

            PropertyInfo localizerProperty = typeof(TProvider).GetProperty("T", typeof(Localizer));
            if (localizerProperty != null)
            {
                localizerProperty.SetValue(Provider, NullLocalizer.Instance, null);
            }
        } 
#endif

        public TProvider Provider { get; private set; }

#if (NUNIT)
        [SetUp]
        public void Setup()
        {
            Provider = new TProvider();

            PropertyInfo localizerProperty = typeof(TProvider).GetProperty("T", typeof(Localizer));
            if (localizerProperty != null)
            {
                localizerProperty.SetValue(Provider, NullLocalizer.Instance, null);
            }
        }
#endif

        protected MenuItem GetMenuItem(params string[] path)
        {
            if (path.Length == 0)
            {
                return null;
            }

            var builder = new NavigationBuilder();
            Provider.GetNavigation(builder);

            IEnumerable<MenuItem> menuItems = builder.Build();

            MenuItem item = null;
            foreach (string name in path)
            {
                item = menuItems.SingleOrDefault(m => m.Text.Text == name);
                if (item == null)
                {
                    GenericAssert.Fail("Failed to find menu item '" + name + "'.");
                }

                menuItems = item.Items;
            }

            return item;
        }

        protected void AssertMenuItem(MenuItem item, string area, string controller, string action)
        {
            GenericAssert.AreEqual(area, item.RouteValues["area"]);
            GenericAssert.AreEqual(controller, item.RouteValues["controller"]);
            GenericAssert.AreEqual(action, item.RouteValues["action"]);
        }

        protected void AssertMenuItemPermissions(MenuItem localRolesItem, params Permission[] permissions)
        {
            GenericAssert.CollectionsAreEqual(permissions, localRolesItem.Permissions.ToArray());
        }
    }
}