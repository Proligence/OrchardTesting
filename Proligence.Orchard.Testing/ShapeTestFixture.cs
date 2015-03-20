namespace Proligence.Orchard.Testing
{
#if (NUNIT)
    using NUnit.Framework;
#endif
    using Proligence.Orchard.Testing.Mocks;

    public abstract class ShapeTestFixture
    {
        public ShapeFactoryMock ShapeFactory { get; private set; }

#if (NUNIT)
        [SetUp]
        public virtual void Setup()
        {
            ShapeFactory = new ShapeFactoryMock();
        }
#endif

#if XUNIT
        protected ShapeTestFixture()
        {
            ShapeFactory = new ShapeFactoryMock();
        }
#endif
    }
}