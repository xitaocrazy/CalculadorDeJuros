using NLog;

namespace CalculadorDeJurosApi.Wrappers
{
    public class LoggerManager : ILoggerManager
    {
        private ILogger logger;

        public ILogger Logger
        {
            get
            {
                if (logger == null){
                    logger = LogManager.GetCurrentClassLogger();
                }
                return logger;
            }

            set => logger = value;
        }
        

        public void LogDebug(string message)
        {
            Logger.Debug(message);
        }

        public void LogError(string message)
        {
            Logger.Error(message);
        }

        public void LogInfo(string message)
        {
            Logger.Info(message);
        }

        public void LogWarn(string message)
        {
            Logger.Warn(message);
        }
    }
}
