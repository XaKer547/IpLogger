using IpLogger.Domain.Filters;
using IpLogger.Domain.Models;

namespace IpLogger.Domain.Interfaces
{
    public interface ILogService
    {
        IReadOnlyCollection<LogDTO> GetLogs();
        IEnumerable<LogDTO> FilterLogs(IReadOnlyCollection<LogDTO> logs, LogFilter? filter = null);
    }
}