

namespace disclone_api.Services
{
    public interface ILoggerService
    {
        public void LogMessage(string message);
        public void LogMessage(Exception ex);
    }
}
