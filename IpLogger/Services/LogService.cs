using IpLogger.Console.Filters;
using IpLogger.Models;
using IpLogger.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Data.SqlTypes;

namespace IpLogger.Services
{
    public class LogService(ILogger logger) : ILogService
    {
        public async Task<IEnumerable<Log>> GetLogsAsync(string path, LoggerFilter? filter = null)
        {
            uint skippedLines = 0;

            var lines = await File.ReadAllLinesAsync(path);

            var logs = new List<Log>();

            foreach (var line in lines)
            {
                if (!Log.TryParse(line, out var log))
                {
                    skippedLines++;

                    logger.LogWarning("Строка не может быть прочитана и будет пропущена");

                    continue;
                }

                logs.Add(log);
            }

            logger.LogInformation($"Прочитано строк: {logs.Count}. Пропущено: {skippedLines}");

            if (filter is null)
                return logs;

            var predicates = filter.GetPredicates()
            .ToArray();

            return logs.Where(l => predicates.Any(p => p(l)));
        }

        public async Task SaveLogsAsync(IEnumerable<Log> logs, string path)
        {
            using var stream = new StreamWriter(path);

            uint writtenLogs = 0;

            foreach (var log in logs)
            {
                await stream.WriteLineAsync(log.ToString());

                writtenLogs++;
            }

            logger.LogInformation($"Сохранено логов: {writtenLogs}");
        }
    }
}