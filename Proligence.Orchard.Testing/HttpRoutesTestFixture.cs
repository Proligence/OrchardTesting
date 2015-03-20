namespace Proligence.Orchard.Testing
{
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
#if (NUNIT)
    using NUnit.Framework;
#endif
    using global::Orchard.Mvc.Routes;
    using global::Orchard.WebApi.Routes;
#if (XUNIT)
    using Xunit;
#endif

    public abstract class HttpRoutesTestFixture<TRouteProvider>
        where TRouteProvider : IHttpRouteProvider, new()
    {
#if (XUNIT)
        protected HttpRoutesTestFixture()
        {
            RouteProvider = new TRouteProvider();
        } 
#endif

        public TRouteProvider RouteProvider { get; private set; }

#if (NUNIT)
        [SetUp]
#endif
        public void Setup()
        {
            RouteProvider = new TRouteProvider();
        }

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
                var routeDescriptor1 = (HttpRouteDescriptor)routes1[i];
                var routeDescriptor2 = (HttpRouteDescriptor)routes1[i];
                
                GenericAssert.AreEqual(routes2[i].Name, routes1[i].Name);
                GenericAssert.AreEqual(routes2[i].Priority, routes1[i].Priority);
                GenericAssert.AreEqual(routeDescriptor2.RouteTemplate, routeDescriptor1.RouteTemplate);
                GenericAssert.AreEqual(routeDescriptor2.Defaults, routeDescriptor1.Defaults);
            }
        }

        protected void AssertRoute(string url, string area, string controller, string action)
        {
            RouteDescriptor routeDescriptor = RouteProvider
                .GetRoutes()
                .SingleOrDefault(r => ((HttpRouteDescriptor)r).RouteTemplate == url);

            if (routeDescriptor == null)
            {
                GenericAssert.Fail("Failed to find route with URL '" + url + "'.");
            }

            var route = (HttpRouteDescriptor)routeDescriptor;
            
            string expected = string.Format(
                CultureInfo.InvariantCulture,
                "{{ area = {0}, controller = {1}, action = {2} }}",
                area, 
                controller,
                action);

            GenericAssert.AreEqual(expected, route.Defaults.ToString());
        }
    }
}