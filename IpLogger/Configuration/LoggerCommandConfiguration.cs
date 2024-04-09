using IpLogger.Console.Filters;
using IpLogger.Services.Interfaces;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Net;

namespace IpLogger.Configuration
{
    public class LoggerCommandConfiguration(ILogService logService) : ICommandConfiguration
    {
        public RootCommand ConfigureRootCommand()
        {
            var fileLog = new Option<FileInfo>("--file-log", "Путь к файлу с логами")
            {
                IsRequired = true,
            };

            fileLog.AddValidator(result =>
            {
                FileInfo file = result.GetValueForOption(fileLog)!;

                if (!file.Exists)
                {
                    result.ErrorMessage = "Файл с логами не найден или не существует";
                }
            });

            var fileOutput = new Option<FileInfo>("--file-output", "Путь к файлу с результатом")
            {
                IsRequired = true,
            };

            fileOutput.AddValidator(result =>
            {
                FileInfo file = result.GetValueForOption(fileOutput)!;

                if (!file.Directory.Exists)
                    result.ErrorMessage = "Не удалось создать конечный файл";
            });

            var addressStart = new Option<IPAddress>("--address-start", "Нижняя граница диапазона адресов, по умолчанию обрабатываются все адреса");

            var addressMask = new Option<int>("--address-mask", "Маска подсети, задающая верхнюю границу диапазона десятичное число. " +
                "В случае, если он не указан, обрабатываются все адреса, начиная с нижней границы диапазона. Параметр нельзя использовать, если не задан address-start.");

            addressMask.AddValidator(result =>
            {
                var address = result.GetValueForOption(addressStart)!;

                if (address is null)
                {
                    result.ErrorMessage = "Отсутсвует параметр --address-start";
                }
            });

            var timeStart = new Option<DateOnly>("--time-start", "Нижняя граница временного интервала");

            var timeEnd = new Option<DateOnly>("--time-end", "Верхняя граница временного интервала");

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
                    var filter = new LoggerFilter()
                    {
                        AddressStart = addressStartValue,
                        Cidr = addressMaskValue,
                        TimeStart = timeStartValue,
                        TimeEnd = timeEndValue,
                    };

                    var logs = logService.GetLogs(fileLogValue.FullName, filter);

                    logService.SaveLogs(logs, fileOutputValue.FullName);
                },
                fileLog, fileOutput, addressStart, addressMask, timeStart, timeEnd);

            return rootCommand;
        }

        public IEnumerable<Option> GetOptions()
        {
            var fileLog = new Option<FileInfo>("--file-log", "Путь к файлу с логами")
            {
                IsRequired = true,
            };

            fileLog.AddValidator(result =>
            {
                FileInfo file = result.GetValueForOption(fileLog)!;

                if (!file.Exists)
                {
                    result.ErrorMessage = "Файл с логами не найден или не существует";
                }
            });

            var fileOutput = new Option<FileInfo>("--file-output", "Путь к файлу с результатом")
            {
                IsRequired = true,
            };

            fileOutput.AddValidator(result =>
            {
                FileInfo file = result.GetValueForOption(fileOutput)!;

                try
                {
                    using (file.Create()) { }

                    file.Delete();
                }
                catch
                {
                    result.ErrorMessage = "Не удалось создать конечный файл";
                }
            });

            var addressStart = new Option<IPAddress>("--address-start", "Нижняя граница диапазона адресов, по умолчанию обрабатываются все адреса");

            var addressMask = new Option<int>("--address-mask", "Маска подсети, задающая верхнюю границу диапазона десятичное число. " +
                "В случае, если он не указан, обрабатываются все адреса, начиная с нижней границы диапазона. Параметр нельзя использовать, если не задан address-start.");

            addressMask.AddValidator(result =>
            {
                var address = result.GetValueForOption(addressStart)!;

                if (address is null)
                {
                    result.ErrorMessage = "Отсутсвует параметр --address-start";
                }
            });

            var timeStart = new Option<DateOnly>("--time-start", "Нижняя граница временного интервала");

            var timeEnd = new Option<DateOnly>("--time-end", "Верхняя граница временного интервала");

            var options = new List<Option>()
            {
                fileLog,
                fileOutput,
                addressStart,
                addressMask,
                timeStart,
                timeEnd,
            };

            return options;
        }

        public Action<InvocationContext> GetHandler()
        {
            var handler = (fileLogValue, fileOutputValue, addressStartValue, addressMaskValue, timeStartValue, timeEndValue) =>
            {
                var filter = new LoggerFilter()
                {
                    AddressStart = addressStartValue,
                    Cidr = addressMaskValue,
                    TimeStart = timeStartValue,
                    TimeEnd = timeEndValue,
                };

                var logs = logService.GetLogs(fileLogValue.FullName, filter);

                logService.SaveLogs(logs, fileOutputValue.FullName);
            }
            ,
                fileLog, fileOutput, addressStart, addressMask, timeStart, timeEnd);
        }
    }
}