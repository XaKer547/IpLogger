﻿using IpLogger.Console.Filters;
using IpLogger.Models;
using IpLogger.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace IpLogger.Services
{
    public class LogService(ILogger logger) : ILogService
    {
        public IEnumerable<Log> GetLogs(string path, LoggerFilter filter)
        {
            uint skippedLines = 0;

            var lines = File.ReadLines(path);

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

            var predicates = filter.GetPredicates()
            .ToArray();

            return logs.Where(l => predicates.Any(p => p(l)));
        }

        public void SaveLogs(IEnumerable<Log> logs, string path)
        {
            using var stream = new StreamWriter(path);

            foreach (var log in logs)
                stream.WriteLine(log);
        }
    }
}