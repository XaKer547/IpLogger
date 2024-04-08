using IpLogger.Console.Filters;
using IpLogger.Models;

namespace IpLogger.Services.Interfaces
{
    public interface ILogService
    {
        IEnumerable<Log> FilterLogs(IEnumerable<Log> logs, LoggerFilter filter);
        IEnumerable<Log> GetLogs(string path);
        void SaveLogs(IEnumerable<Log> logs, string path);
    }
}