using IpLogger.Domain.Models;
using System.IO.Abstractions;

namespace IpLogger.Console.Infrastucture
{
    public class LogWriter(IFileSystem fileSystem)
    {
        public void SaveLogs(IReadOnlyCollection<LogDTO> logs, string path)
        {
            using var stream = fileSystem.File.CreateText(path);

            foreach (var log in logs)
                stream.WriteLine(log);
        }
    }
}
