namespace Proligence.Orchard.Testing
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Web.Routing;
    using NUnit.Framework;
    using global::Orchard.Mvc.Routes;

    public abstract class RoutesTestFixture<TRouteProvider>
        where TRouteProvider : IRouteProvider, new()
    {
        public TRouteProvider RouteProvider { get; private set; }

        [SetUp]
        public void Setup()
        {
            RouteProvider = new TRouteProvider();
        }

        [Test]
        public void TestLegacyGetRoutes()
        {
            RouteDescriptor[] routes1 = RouteProvider.GetRoutes().ToArray();

            var routeCollection = new Collection<RouteDescriptor>();
            RouteProvider.GetRoutes(routeCollection);
            
            RouteDescriptor[] routes2 = routeCollection.ToArray();

            Assert.That(routes1.Length, Is.EqualTo(routes2.Length));

            for (int i = 0; i < routes1.Length; i++)
            {
                Assert.That(routes1[i].Name, Is.EqualTo(routes2[i].Name));
                Assert.That(routes1[i].Priority, Is.EqualTo(routes2[i].Priority));
                AssertEqualRoutes((Route)routes1[i].Route, (Route)routes2[i].Route);
            }
        }

        protected void AssertRoute(string url, string area, string controller, string action)
        {
            RouteDescriptor routeDescriptor = RouteProvider
                .GetRoutes()
                .SingleOrDefault(r => ((Route)r.Route).Url == url);

            if (routeDescriptor == null)
            {
                Assert.Fail("Failed to find route with URL '" + url + "'.");
            }

            var route = (Route)routeDescriptor.Route;
            Assert.That(route.Defaults["area"], Is.EqualTo(area));
            Assert.That(route.Defaults["controller"], Is.EqualTo(controller));
            Assert.That(route.Defaults["action"], Is.EqualTo(action));
            Assert.That(route.DataTokens["area"], Is.EqualTo(area));
        }

        private static void AssertEqualRoutes(Route route1, Route route2)
        {
            Assert.That(route1.Url, Is.EqualTo(route2.Url));
            Assert.That(route1.RouteHandler.GetType(), Is.EqualTo(route2.RouteHandler.GetType()));
            AssertEqualDictionaries(route1.Constraints, route2.Constraints);
            AssertEqualDictionaries(route1.DataTokens, route2.DataTokens);
            AssertEqualDictionaries(route1.Defaults, route2.Defaults);
        }

        private static void AssertEqualDictionaries(RouteValueDictionary dictionary1, RouteValueDictionary dictionary2)
        {
            Assert.That(dictionary1.Count, Is.EqualTo(dictionary2.Count));

            foreach (KeyValuePair<string, object> entry in dictionary1)
            {
                Assert.That(dictionary2[entry.Key], Is.EqualTo(entry.Value));
            }
        }
    }
}