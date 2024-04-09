using System.CommandLine;

namespace IpLogger.Commands
{
    public class LoggerCommand : RootCommand
    {
        public LoggerCommand()
        {
            Description = "вывести в файл список IP-адресов из файла журнала, входящих в указанный диапазон с количеством обращений с этого адреса в указанный интервал времени";
        }
    }
}
