namespace Proligence.Orchard.Testing
{
    using Proligence.Orchard.Testing.Mocks;

    public class ContentDriverTestFixture : ShapeTestFixture
    {
        public UpdateModelMock Updater { get; private set; }

        public override void Setup()
        {
            base.Setup();
            Updater = new UpdateModelMock();
        }
    }
}