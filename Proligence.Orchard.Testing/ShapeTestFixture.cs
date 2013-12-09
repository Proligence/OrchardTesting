namespace Proligence.Orchard.Testing
{
    using NUnit.Framework;
    using Proligence.Orchard.Testing.Mocks;

    public abstract class ShapeTestFixture
    {
        public ShapeFactoryMock ShapeFactory { get; private set; }

        [SetUp]
        public virtual void Setup()
        {
            ShapeFactory = new ShapeFactoryMock();
        }
    }
}