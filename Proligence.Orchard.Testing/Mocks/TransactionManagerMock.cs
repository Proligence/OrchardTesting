namespace Proligence.Orchard.Testing.Mocks
{
    using Moq;
    using global::Orchard.Data;

    public class TransactionManagerMock : Mock<ITransactionManager>
    {
        public TransactionManagerMock()
        {
        }

        public TransactionManagerMock(MockBehavior mockBehavior)
            : base(mockBehavior)
        {
        }

        public void ExpectDemand()
        {
            Setup(x => x.Demand());
        }

        public void ExpectCancel()
        {
            Setup(x => x.Cancel());
        }
    }
}