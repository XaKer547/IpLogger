using IpLogger.DataAccess.Helpers;
using IpLogger.Models;
using System.IO.Abstractions;

namespace IpLogger.DataAccess.Data
{
    public class LogProvider(string path, IFileSystem fileSystem)
    {
        public IReadOnlyCollection<Log> Logs => GetLogs();
        private IReadOnlyCollection<Log> GetLogs()
        {
            var lines = fileSystem.File.ReadAllLines(path);

            var logs = new List<Log>();

            foreach (var line in lines)
            {
                if (!LogHelper.TryParse(line, out var log))
                    continue;

                logs.Add(log);
            }

            return logs;
        }
    }
}
