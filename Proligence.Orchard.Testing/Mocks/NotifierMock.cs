namespace Proligence.Orchard.Testing.Mocks
{
    using Moq;
    using global::Orchard.Localization;
    using global::Orchard.UI.Notify;

    public class NotifierMock : Mock<INotifier>
    {
        public NotifierMock()
        {
        }

        public NotifierMock(MockBehavior mockBehavior)
            : base(mockBehavior)
        {
        }

        public void ExpectError(string message)
        {
            Setup(x => x.Add(NotifyType.Error, It.Is<LocalizedString>(s => s.Text == message)));
        }

        public void ExpectWarning(string message)
        {
            Setup(x => x.Add(NotifyType.Warning, It.Is<LocalizedString>(s => s.Text == message)));
        }

        public void ExpectInformation(string message)
        {
            Setup(x => x.Add(NotifyType.Information, It.Is<LocalizedString>(s => s.Text == message)));
        }
    }
}