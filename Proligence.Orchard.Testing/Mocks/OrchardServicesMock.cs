namespace Proligence.Orchard.Testing.Mocks
{
    using global::Orchard;
    using global::Orchard.ContentManagement;
    using global::Orchard.Data;
    using global::Orchard.Security;
    using global::Orchard.UI.Notify;

    public class OrchardServicesMock : IOrchardServices
    {
        public OrchardServicesMock()
        {
            ContentManagerMock = new ContentManagerMock();
            TransactionManagerMock = new TransactionManagerMock();
            AuthorizerMock = new AuthorizerMock();
            NotifierMock = new NotifierMock();
            WorkContextMock = new WorkContextMock();
        }

        public ContentManagerMock ContentManagerMock { get; private set; }
        public TransactionManagerMock TransactionManagerMock { get; private set; }
        public AuthorizerMock AuthorizerMock { get; private set; }
        public NotifierMock NotifierMock { get; private set; }
        public WorkContextMock WorkContextMock { get; private set; }

        public IContentManager ContentManager
        {
            get { return ContentManagerMock.Object; }
        }

        public ITransactionManager TransactionManager
        {
            get { return TransactionManagerMock.Object; }
        }

        public IAuthorizer Authorizer
        {
            get { return AuthorizerMock.Object; }
        }

        public INotifier Notifier
        {
            get { return NotifierMock.Object; }
        }
        
        public dynamic New { get; set; }
        
        public WorkContext WorkContext
        {
            get { return WorkContextMock; }
        }
    }
}