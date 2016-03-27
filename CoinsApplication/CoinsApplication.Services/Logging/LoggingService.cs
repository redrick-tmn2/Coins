using System;
using System.Reflection;
using CoinsApplication.Services.Interfaces.Logging;
using log4net;

namespace CoinsApplication.Services.Logging
{
    public class LoggingService : ILoggingService
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Error(string message)
        {
            _log.Error(message);
        }

        public void Error(string message, Exception ex)
        {
            _log.Error(message, ex);
        }
    }
}
