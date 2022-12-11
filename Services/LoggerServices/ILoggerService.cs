

namespace disclone_api.Services.LoggerServices
{
    public interface ILoggerService
    {
        public void LogMessage(string message);
        public void LogMessage(Exception ex);
    }
}
