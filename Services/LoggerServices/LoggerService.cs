using Microsoft.Extensions.Logging;

namespace disclone_api.Services.LoggerServices
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
            Console.WriteLine("a");
            _logger.LogInformation(message);   
        }

        public void LogMessage(Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, \n {ex.StackTrace}");
        }
    }
}