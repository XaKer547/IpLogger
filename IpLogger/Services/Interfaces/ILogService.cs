using IpLogger.Console.Filters;
using IpLogger.Models;

namespace IpLogger.Services.Interfaces
{
    public interface ILogService
    {
        IEnumerable<Log> GetLogs(string path, LoggerFilter filter);
        void SaveLogs(IEnumerable<Log> logs, string path);
    }
}