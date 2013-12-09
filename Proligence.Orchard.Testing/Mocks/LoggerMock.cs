namespace Proligence.Orchard.Testing.Mocks
{
    using System;
    using Moq;
    using global::Orchard.Logging;

    public class LoggerMock : Mock<ILogger>
    {
        public LoggerMock()
        {
            this.InitializeMock();
        }

        public LoggerMock(MockBehavior mockBehavior)
            : base(mockBehavior)
        {
            this.InitializeMock();
        }

        public void ExpectFatalError(string expectedMessage)
        {
            Setup(x => x.Log(LogLevel.Fatal, null, expectedMessage, null));
        }

        public void ExpectFatalError(string expectedMessage, Exception exception)
        {
            Setup(x => x.Log(LogLevel.Fatal, exception, expectedMessage, null));
        }

        public void ExpectFatalError(string expectedMessage, Exception exception, params object[] args)
        {
            Setup(x => x.Log(LogLevel.Fatal, exception, expectedMessage, args));
        }

        public void ExpectError(string expectedMessage)
        {
            Setup(x => x.Log(LogLevel.Error, null, expectedMessage, null));
        }

        public void ExpectError(string expectedMessage, Exception exception)
        {
            Setup(x => x.Log(LogLevel.Error, exception, expectedMessage, null));
        }

        public void ExpectError(string expectedMessage, Exception exception, params object[] args)
        {
            Setup(x => x.Log(LogLevel.Error, exception, expectedMessage, args));
        }

        public void ExpectWarning(string expectedMessage)
        {
            Setup(x => x.Log(LogLevel.Warning, null, expectedMessage, null));
        }

        public void ExpectWarning(string expectedMessage, params object[] args)
        {
            Setup(x => x.Log(LogLevel.Warning, null, expectedMessage, args));
        }

        public void ExpectInformation(string expectedMessage)
        {
            Setup(x => x.Log(LogLevel.Information, null, expectedMessage, null));
        }

        public void ExpectInformation(string expectedMessage, params object[] args)
        {
            Setup(x => x.Log(LogLevel.Information, null, expectedMessage, args));
        }

        public void ExpectDebug(string expectedMessage)
        {
            Setup(x => x.Log(LogLevel.Debug, null, expectedMessage, null));
        }

        public void ExpectDebug(string expectedMessage, params object[] args)
        {
            Setup(x => x.Log(LogLevel.Debug, null, expectedMessage, args));
        }

        private void InitializeMock()
        {
            this.Setup(x => x.IsEnabled(It.IsAny<LogLevel>())).Returns(true);

            // Invoke the property to avoid unverified expectations.
            this.Object.IsEnabled(LogLevel.Debug);
        }
    }
}