using IpLogger.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;

namespace IpLogger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Console.Hosting.Configuration.CreateHostBuilder(args)
                .Build();

            var command = host.Services.GetRequiredService<LoggerCommand>();

            command.Invoke(args);
        }
    }
}