using IpLogger.DataAccess.Data;
using IpLogger.Domain.Filters;
using IpLogger.Domain.Interfaces;
using IpLogger.Domain.Models;
using Microsoft.Extensions.Logging;

namespace IpLogger.Application.Services
{
    public class LogService(LogProvider provider, ILogger logger) : ILogService
    {
        public IEnumerable<LogDTO> FilterLogs(IReadOnlyCollection<LogDTO> logs, LogFilter? filter = null)
        {
            if (filter is null)
                return logs;

            var predicates = filter.GetPredicates()
            .ToArray();

            return logs.Where(l => predicates.Any(p => p(l)));
        }

        public IReadOnlyCollection<LogDTO> GetLogs()
        {
            var logs = provider.Logs.Select(l => new LogDTO(l.Address, l.LastAccessedTime))
                .ToArray();

            logger.LogInformation($"Получено логгов: {logs.Length}");

            return logs;
        }
    }
}