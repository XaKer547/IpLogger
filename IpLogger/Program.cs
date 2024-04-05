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

            fileLog.AddValidator(v =>
            {
                FileInfo value = v.GetValueForOption(fileLog)!;

                if (!value.Exists)
                    v.ErrorMessage = "Файл с логами не найден или не существует";
            });

            var fileOutput = new Option<FileInfo>("--file-output", "Путь к файлу с результатом")
            {
                IsRequired = true,
            };

            var addressStart = new Option<string>("--address-start", "Нижняя граница диапазона адресов, по умолчанию обрабатываются все адреса");

            var addressMask = new Option<string>("--address-mask", "Маска подсети, задающая верхнюю границу диапазона десятичное число. " +
                "В случае, если он не указан, обрабатываются все адреса, начиная с нижней границы диапазона. Параметр нельзя использовать, если не задан address-start.");

            var timeStart = new Option<string>("--time-start", "Нижняя граница временного интервала")
            {
                IsRequired = true
            };

            var timeEnd = new Option<string>("--time-end", "Верхняя граница временного интервала")
            {
                IsRequired = true
            };

            var rootCommand = new RootCommand()
            {
                fileLog,
                fileOutput,
                addressStart,
                addressMask,
                timeStart,
                timeEnd
            };

            rootCommand.SetHandler(
                (fileLogValue, fileOutputValue, addressStartValue, addressMaskValue, timeStartValue, timeEndValue) =>
                {
                    ////отфильтровать данные файла
                    ////сохранить файл

                    uint skippedLines = 0;

                    var logs = GetLogs(fileLogValue.FullName, ref skippedLines);



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
                    Console.WriteLine("Строка не может быть обработана и будет пропущена");
                    skippedLines++;
                    continue;
                }

                logs.Add(log);
            }

            return [.. logs];
        }

        private static Log[] FilterByAddress(Log[] logs, IPAddress? addressStart, IPAddress? addressMask)
        {






        }
    }
}
