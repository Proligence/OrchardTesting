namespace Proligence.Orchard.Testing
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using NUnit.Framework;
    using global::Orchard.Localization;
    using global::Orchard.Security.Permissions;
    using global::Orchard.UI.Navigation;

    public abstract class NavigationProviderTestFixture<TProvider>
        where TProvider : INavigationProvider, new()
    {
        public TProvider Provider { get; private set; }

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
                    Assert.Fail("Failed to find menu item '" + name + "'.");
                }

                menuItems = item.Items;
            }

            return item;
        }

        protected void AssertMenuItem(MenuItem item, string area, string controller, string action)
        {
            Assert.That(item.RouteValues["area"], Is.EqualTo(area));
            Assert.That(item.RouteValues["controller"], Is.EqualTo(controller));
            Assert.That(item.RouteValues["action"], Is.EqualTo(action));
        }

        protected void AssertMenuItemPermissions(MenuItem localRolesItem, params Permission[] permissions)
        {
            Assert.That(localRolesItem.Permissions.ToArray(), Is.EquivalentTo(permissions));
        }
    }
}