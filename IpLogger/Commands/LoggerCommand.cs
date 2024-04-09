using IpLogger.Console.Helpers;
using IpLogger.Services.Interfaces;
using System.CommandLine;

namespace IpLogger.Commands
{
    public class LoggerCommand : RootCommand
    {
        public LoggerCommand(ILogService logService)
        {
            Description = "вывести в файл список IP-адресов из файла журнала," +
                " входящих в указанный диапазон с количеством обращений с этого адреса в указанный интервал времени";

            _ = this.AddFileLogOption()
                .AddFileOututOption()
                .AddIPAddressOption()
                .AddTimeIntrevalOptions()
                .AddLoggerHandler(logService);
        }
    }
}
