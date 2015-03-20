namespace Proligence.Orchard.Testing
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Web.Routing;
#if (NUNIT)
    using NUnit.Framework;
#elif (XUNIT)
    using Xunit;
#endif
    using global::Orchard.Mvc.Routes;

    public abstract class RoutesTestFixture<TRouteProvider>
        where TRouteProvider : IRouteProvider, new()
    {
#if (XUNIT)
        protected RoutesTestFixture()
        {
            RouteProvider = new TRouteProvider();
        } 
#endif

        public TRouteProvider RouteProvider { get; private set; }

#if (NUNIT)
        [SetUp]
        public void Setup()
        {
            RouteProvider = new TRouteProvider();
        }
#endif

#if (NUNIT)
        [Test]
#elif (XUNIT)
        [Fact]
#endif
        public void TestLegacyGetRoutes()
        {
            RouteDescriptor[] routes1 = RouteProvider.GetRoutes().ToArray();

            var routeCollection = new Collection<RouteDescriptor>();
            RouteProvider.GetRoutes(routeCollection);
            
            RouteDescriptor[] routes2 = routeCollection.ToArray();

            GenericAssert.AreEqual(routes2.Length, routes1.Length);

            for (int i = 0; i < routes1.Length; i++)
            {
                GenericAssert.AreEqual(routes2[i].Name, routes1[i].Name);
                GenericAssert.AreEqual(routes2[i].Priority, routes1[i].Priority);
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
                GenericAssert.Fail("Failed to find route with URL '" + url + "'.");
            }

            var route = (Route)routeDescriptor.Route;
            GenericAssert.AreEqual(area, route.Defaults["area"]);
            GenericAssert.AreEqual(controller, route.Defaults["controller"]);
            GenericAssert.AreEqual(action, route.Defaults["action"]);
            GenericAssert.AreEqual(area, route.DataTokens["area"]);
        }

        private static void AssertEqualRoutes(Route route1, Route route2)
        {
            GenericAssert.AreEqual(route2.Url, route1.Url);
            GenericAssert.AreEqual(route2.RouteHandler.GetType(), route1.RouteHandler.GetType());
            AssertEqualDictionaries(route1.Constraints, route2.Constraints);
            AssertEqualDictionaries(route1.DataTokens, route2.DataTokens);
            AssertEqualDictionaries(route1.Defaults, route2.Defaults);
        }

        private static void AssertEqualDictionaries(RouteValueDictionary dictionary1, RouteValueDictionary dictionary2)
        {
            GenericAssert.AreEqual(dictionary2.Count, dictionary1.Count);

            foreach (KeyValuePair<string, object> entry in dictionary1)
            {
                GenericAssert.AreEqual(entry.Value, dictionary2[entry.Key]);
            }
        }
    }
}