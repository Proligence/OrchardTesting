namespace Proligence.Orchard.Testing.Mocks
{
    using Moq;
    using global::Orchard.Data;

    public class TransactionManagerMock : Mock<ITransactionManager>
    {
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