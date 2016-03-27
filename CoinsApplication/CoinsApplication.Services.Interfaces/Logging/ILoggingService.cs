using System;

namespace CoinsApplication.Services.Interfaces.Logging
{
    public interface ILoggingService
    {
        void Error(string message);

        void Error(string message, Exception ex);
    }
}
