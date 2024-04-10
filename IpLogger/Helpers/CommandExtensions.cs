using IpLogger.Console.Commands.Enums;
using IpLogger.Console.Infrastucture;
using IpLogger.Console.Options;
using IpLogger.Domain.Filters;
using IpLogger.Domain.Interfaces;
using System.CommandLine;

namespace IpLogger.Console.Helpers
{
    public static class CommandExtensions
    {
        public static Command AddLogHandler(this Command Command, ILogService logService, LogWriter logWriter)
        {
            Command.SetHandler(context =>
            {
                var addressStart = context.ParseResult.GetValueForOption(OptionDefaults.AddressStart);

                var addressMask = context.ParseResult.GetValueForOption(OptionDefaults.AddressMask);

                var timeStart = context.ParseResult.GetValueForOption(OptionDefaults.TimeStart);

                var timeEnd = context.ParseResult.GetValueForOption(OptionDefaults.TimeEnd);

                var filter = new LogFilter()
                {
                    AddressStart = addressStart,
                    Cidr = addressMask,
                    TimeStart = timeStart,
                    TimeEnd = timeEnd,
                };

                var logs = logService.GetLogs();

                var fileOutput = context.ParseResult.GetValueForOption(OptionDefaults.FileOutput)!;

                var filterdLogs = logService.FilterLogs(logs, filter);

                logWriter.SaveLogs(logs, fileOutput.FullName);

                ExitCodeManager.ExitCode = ExitCodes.Success;
            });

            return Command;
        }
    }
}
