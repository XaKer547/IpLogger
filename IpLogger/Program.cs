using IpLogger.Helpers;
using IpLogger.Models;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Net;

namespace IpLogger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fileLog = new Option<FileInfo>("--file-log", "Путь к файлу с логами")
            {
                IsRequired = true,
            };

            fileLog.AddValidator(result =>
            {
                FileInfo file = result.GetValueForOption(fileLog)!;

                if (!file.Exists)
                    result.ErrorMessage = "Файл с логами не найден или не существует";
            });

            var fileOutput = new Option<FileInfo>("--file-output", "Путь к файлу с результатом")
            {
                IsRequired = true,
            };

            var addressStart = new Option<IPAddress>("--address-start", "Нижняя граница диапазона адресов, по умолчанию обрабатываются все адреса");

            var addressMask = new Option<int>("--address-mask", "Маска подсети, задающая верхнюю границу диапазона десятичное число. " +
                "В случае, если он не указан, обрабатываются все адреса, начиная с нижней границы диапазона. Параметр нельзя использовать, если не задан address-start.");

            var timeStart = new Option<DateOnly>("--time-start", "Нижняя граница временного интервала")
            {
                IsRequired = true
            };

            var timeEnd = new Option<DateOnly>("--time-end", "Верхняя граница временного интервала")
            {
                IsRequired = true
            };

            var rootCommand = new RootCommand("вывести в файл список IP-адресов из файла журнала, " +
                "входящих в указанный диапазон с количеством обращений с этого адреса в указанный интервал времени")
            {
                fileLog,
                fileOutput,
                addressStart,
                addressMask,
                timeStart,
                timeEnd,
            };

            rootCommand.SetHandler(
                (fileLogValue, fileOutputValue, addressStartValue, addressMaskValue, timeStartValue, timeEndValue) =>
                {
                    uint skippedLines = 0;

                    var logs = GetLogs(fileLogValue.FullName, ref skippedLines);

                    FilterByDate(ref logs, timeStartValue, timeEndValue);

                    FilterByAddress(ref logs, addressStartValue, addressMaskValue);

                    SaveFile(logs, fileOutputValue.FullName);
                },
                fileLog, fileOutput, addressStart, addressMask, timeStart, timeEnd);

            rootCommand.Invoke(args);
        }

        private static Log[] GetLogs(string path, ref uint skippedLines)
        {
            var lines = File.ReadLines(path);

            var logs = new List<Log>();

            foreach (var line in lines)
            {
                if (!Log.TryParse(line, out var log))
                {
                    skippedLines++;

                    Console.WriteLine("Строка не может быть обработана и будет пропущена");

                    continue;
                }

                logs.Add(log);
            }

            return [.. logs];
        }

        private static void FilterByAddress(ref Log[] logs, IPAddress? addressStart, int addressMask = default)
        {
            //TODO: доделать и провести оптимизацию/рефакторинг

            if (addressMask == default)
                return;

            var mask = CIDRExtensions.CidrToMask(addressMask);

            var filteredLogs = logs.Where(l => l.Address.IsInRange(addressStart, mask))
                .ToArray();

            logs = filteredLogs;
        }

        private static void FilterByDate(ref Log[] logs, DateOnly timeStart, DateOnly timeEnd)
        {
            TimeOnly time = new(0);

            var start = timeStart.ToDateTime(time);

            var end = timeEnd.ToDateTime(time);

            var filteredLogs = logs.Where(l => l.LastAccessedTime > start && l.LastAccessedTime < end)
                .ToArray();

            logs = filteredLogs;
        }

        private static void SaveFile(Log[] logs, string path)
        {
            using var stream = new StreamWriter(path);

            foreach (var log in logs)
                stream.WriteLine(log);

            Console.WriteLine($"Сохранено строк: {logs.Length}");
        }
    }
}
