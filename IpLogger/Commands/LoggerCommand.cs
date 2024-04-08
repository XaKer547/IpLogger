using IpLogger.Commands.Abstract;
using IpLogger.Services.Interfaces;

namespace IpLogger.Commands
{
    public class LoggerCommand(string[] args, ICommandConfiguration configuration) : CommandBase(args, configuration)
    {

    }
}
