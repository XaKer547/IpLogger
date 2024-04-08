using System.CommandLine;

namespace IpLogger.Services.Interfaces
{
    public interface ICommandConfiguration
    {
        RootCommand ConfigureRootCommand();
    }
}
