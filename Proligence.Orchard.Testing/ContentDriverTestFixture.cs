namespace Proligence.Orchard.Testing
{
    using Proligence.Orchard.Testing.Mocks;

    public class ContentDriverTestFixture : ShapeTestFixture
    {
#if (XUNIT)
        protected ContentDriverTestFixture()
        {
            Updater = new UpdateModelMock();
        }
#endif

        public UpdateModelMock Updater { get; private set; }

#if (NUNIT)
        public override void Setup()
        {
            base.Setup();
            Updater = new UpdateModelMock();
        }
#endif
    }
}