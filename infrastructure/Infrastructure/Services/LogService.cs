using System;
using AdventureWorks.Foundation.Services;
using NLog;

namespace AdventureWorks.Infrastructure.Services
{
    public class LogService : ILogService
    {
        private readonly Logger logger;

        public LogService()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public LogService(Type sourceType)
        {
            logger = LogManager.GetCurrentClassLogger(sourceType);
        }


        public void Error(Exception exception)
        {
            logger.ErrorException(exception.Message, exception);
        }
    }
}
