using IpLogger.Services.Interfaces;
using System.CommandLine;

namespace IpLogger.Commands.Abstract
{
    public abstract class CommandBase(string[] args, ICommandConfiguration configuration)
    {
        public int Invoke()
        {
            var command = configuration.ConfigureRootCommand();

            return command.Invoke(args);
        }
    }
}
