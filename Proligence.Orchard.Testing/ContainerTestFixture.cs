namespace Proligence.Orchard.Testing
{
    using Autofac;
#if (NUNIT)
    using NUnit.Framework;
#endif

    public abstract class ContainerTestFixture
    {
#if (XUNIT)
        protected ContainerTestFixture()
        {
            var builder = new ContainerBuilder();
            Register(builder);

            Container = builder.Build();
        }
#endif

        public IContainer Container { get; private set; }

#if (NUNIT)
        [SetUp]
        public virtual void Setup()
        {
            var builder = new ContainerBuilder();
            Register(builder);
            
            Container = builder.Build();
        }
#endif

        protected virtual void Register(ContainerBuilder builder)
        {
        }
    }
}