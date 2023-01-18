using Microsoft.Extensions.Logging;

namespace disclone_api.Services
{
    public class LoggerService: ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;
        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }

        public void LogMessage(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogMessage(Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, \n {ex.StackTrace}");
        }
    }
}