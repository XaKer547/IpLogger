using IpLogger.Services.Interfaces;
using System.CommandLine;

namespace IpLogger.Commands.Abstract
{
    public abstract class CommandBase(string[] args, ICommandConfiguration configuration)
    {
        public void Invoke()
        {
            var command = configuration.ConfigureRootCommand();

            command.Invoke(args);
        }
    }
}
