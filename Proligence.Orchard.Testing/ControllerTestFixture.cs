namespace Proligence.Orchard.Testing
{
    using NUnit.Framework;
    using Proligence.Orchard.Testing.Mocks;

    public abstract class ControllerTestFixture
    {
        [SetUp]
        public virtual void Setup()
        {
            OrchardServices = new OrchardServicesMock();
            LoggerMock = new LoggerMock();
        }

        public OrchardServicesMock OrchardServices { get; private set; }
        public LoggerMock LoggerMock { get; private set; }

        public ContentManagerMock ContentManagerMock
        {
            get { return OrchardServices.ContentManagerMock; }
        }

        public TransactionManagerMock TransactionManagerMock
        {
            get { return OrchardServices.TransactionManagerMock; }
        }

        public AuthorizerMock AuthorizerMock
        {
            get { return OrchardServices.AuthorizerMock; }
        }

        public NotifierMock NotifierMock
        {
            get { return OrchardServices.NotifierMock; }
        }

        public WorkContextMock WorkContextMock
        {
            get { return OrchardServices.WorkContextMock; }
        }
    }
}