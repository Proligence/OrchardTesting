namespace Proligence.Orchard.Testing
{
    using Autofac;
    using Autofac.Core;
    using NUnit.Framework;

    public abstract class ContainerTestFixture
    {
        public IContainer Container { get; private set; }

        [SetUp]
        public virtual void Setup()
        {
            var builder = new ContainerBuilder();
            Register(builder);
            
            Container = builder.Build();
        }

        protected virtual void Register(ContainerBuilder builder)
        {
        }
    }
}