using IpLogger.Console.Filters;
using IpLogger.Console.Options;
using IpLogger.Services.Interfaces;
using System.CommandLine;

namespace IpLogger.Console.Helpers
{
    public static class RootCommandExtensions
    {
        public static RootCommand AddLoggerHandler(this RootCommand rootCommand, ILogService logService)
        {
            rootCommand.SetHandler(async context =>
            {
                FileInfo fileLog = context.ParseResult.GetValueForOption(OptionDefaults.FileLog)!;

                FileInfo fileOutput = context.ParseResult.GetValueForOption(OptionDefaults.FileOutput)!;

                var addressStart = context.ParseResult.GetValueForOption(OptionDefaults.AddressStart);

                var addressMask = context.ParseResult.GetValueForOption(OptionDefaults.AddressMask);

                var timeStart = context.ParseResult.GetValueForOption(OptionDefaults.TimeStart);

                var timeEnd = context.ParseResult.GetValueForOption(OptionDefaults.TimeEnd);

                var filter = new LoggerFilter()
                {
                    AddressStart = addressStart,
                    Cidr = addressMask,
                    TimeStart = timeStart,
                    TimeEnd = timeEnd,
                };

                var logs = await logService.GetLogsAsync(fileLog.FullName, filter);

                await logService.SaveLogsAsync(logs, fileOutput.FullName);
            });

            return rootCommand;
        }
    }
}
