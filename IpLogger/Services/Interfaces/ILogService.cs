using IpLogger.Console.Filters;
using IpLogger.Models;

namespace IpLogger.Services.Interfaces
{
    public interface ILogService
    {
        Task<IEnumerable<Log>> GetLogsAsync(string path, LoggerFilter? filter = null);
        Task SaveLogsAsync(IEnumerable<Log> logs, string path);
    }
}