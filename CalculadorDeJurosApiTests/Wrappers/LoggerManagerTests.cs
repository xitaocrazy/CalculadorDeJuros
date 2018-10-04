using CalculadorDeJurosApi.Wrappers;
using Moq;
using NLog;
using Xunit;

namespace CalculadorDeJurosApiTests.Wrappers
{
    public class LoggerManagerTests
    {
        private const string _mensagem = "mensagem padrão";
        private Mock<ILogger> _loggerMock;   


        [Fact]
        public void LogDebug_deve_chamar_logger_debug()
        {
            Setup();
            var logManager = new LoggerManager();
            logManager.Logger = _loggerMock.Object;

            logManager.LogDebug(_mensagem);

            _loggerMock.Verify(l => l.Debug(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void LogError_deve_chamar_logger_error()
        {
            Setup();
            var logManager = new LoggerManager();
            logManager.Logger = _loggerMock.Object;

            logManager.LogError(_mensagem);

            _loggerMock.Verify(l => l.Error(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void LogInfo_deve_chamar_logger_info()
        {
            Setup();
            var logManager = new LoggerManager();
            logManager.Logger = _loggerMock.Object;

            logManager.LogInfo(_mensagem);

            _loggerMock.Verify(l => l.Info(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void LogWar_deve_chamar_logger_warn()
        {
            Setup();
            var logManager = new LoggerManager();
            logManager.Logger = _loggerMock.Object;

            logManager.LogWarn(_mensagem);

            _loggerMock.Verify(l => l.Warn(It.IsAny<string>()), Times.Once);
        }


        private void Setup()
        {
            _loggerMock = new Mock<ILogger>(MockBehavior.Strict);
            _loggerMock.Setup(l => l.Debug(It.IsAny<string>()));
            _loggerMock.Setup(l => l.Error(It.IsAny<string>()));
            _loggerMock.Setup(l => l.Info(It.IsAny<string>()));
            _loggerMock.Setup(l => l.Warn(It.IsAny<string>()));
        }
    }
}