namespace Proligence.Orchard.Testing
{
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using NUnit.Framework;
    using global::Orchard.Mvc.Routes;
    using global::Orchard.WebApi.Routes;

    public abstract class HttpRoutesTestFixture<TRouteProvider>
        where TRouteProvider : IHttpRouteProvider, new()
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
                var routeDescriptor1 = (HttpRouteDescriptor)routes1[i];
                var routeDescriptor2 = (HttpRouteDescriptor)routes1[i];
                
                Assert.That(routes1[i].Name, Is.EqualTo(routes2[i].Name));
                Assert.That(routes1[i].Priority, Is.EqualTo(routes2[i].Priority));
                Assert.That(routeDescriptor1.RouteTemplate, Is.EqualTo(routeDescriptor2.RouteTemplate));
                Assert.That(routeDescriptor1.Defaults, Is.EqualTo(routeDescriptor2.Defaults));
            }
        }

        protected void AssertRoute(string url, string area, string controller, string action)
        {
            RouteDescriptor routeDescriptor = RouteProvider
                .GetRoutes()
                .SingleOrDefault(r => ((HttpRouteDescriptor)r).RouteTemplate == url);

            if (routeDescriptor == null)
            {
                Assert.Fail("Failed to find route with URL '" + url + "'.");
            }

            var route = (HttpRouteDescriptor)routeDescriptor;
            
            string expected = string.Format(
                CultureInfo.InvariantCulture,
                "{{ area = {0}, controller = {1}, action = {2} }}",
                area, 
                controller,
                action);
            
            Assert.That(route.Defaults.ToString(), Is.EqualTo(expected));
        }
    }
}