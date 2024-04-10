using IpLogger.Console.Helpers;
using IpLogger.Console.Infrastucture;
using IpLogger.Domain.Interfaces;
using System.CommandLine;

namespace IpLogger.Console.Commands
{
    public class LogRootCommand : RootCommand
    {
        public LogRootCommand(ILogService logService, LogWriter logWriter)
        {
            Description = "вывести в файл список IP-адресов из файла журнала," +
                " входящих в указанный диапазон с количеством обращений с этого адреса в указанный интервал времени";

            _ = this.AddFileLogOption()
                .AddFileOututOption()
                .AddIPAddressOptions()
                .AddTimeIntrevalOptions()
                .AddLogHandler(logService, logWriter);
        }
    }
}
