namespace Proligence.Orchard.Testing.Mocks
{
    using System;
    using Moq;
    using global::Orchard.Logging;

    public class LoggerMock : Mock<ILogger>
    {
        public LoggerMock()
        {
            Setup(x => x.IsEnabled(It.IsAny<LogLevel>())).Returns(true);

            // Invoke the property to avoid unverified expectations.
            Object.IsEnabled(LogLevel.Debug);
        }

        public void ExpectFatalError(string expectedMessage)
        {
            Setup(x => x.Log(LogLevel.Fatal, null, expectedMessage, null));
        }

        public void ExpectFatalError(string expectedMessage, Exception exception)
        {
            Setup(x => x.Log(LogLevel.Fatal, exception, expectedMessage, null));
        }

        public void ExpectError(string expectedMessage)
        {
            Setup(x => x.Log(LogLevel.Error, null, expectedMessage, null));
        }

        public void ExpectError(string expectedMessage, Exception exception)
        {
            Setup(x => x.Log(LogLevel.Error, exception, expectedMessage, null));
        }

        public void ExpectWarning(string expectedMessage)
        {
            Setup(x => x.Log(LogLevel.Warning, null, expectedMessage, null));
        }

        public void ExpectInformation(string expectedMessage)
        {
            Setup(x => x.Log(LogLevel.Information, null, expectedMessage, null));
        }

        public void ExpectDebug(string expectedMessage)
        {
            Setup(x => x.Log(LogLevel.Debug, null, expectedMessage, null));
        }
    }
}