using IpLogger.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;

namespace IpLogger
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var host = Console.Hosting.Configuration.CreateHostBuilder()
                .Build();

            var command = host.Services.GetRequiredService<LoggerCommand>();

            await command.InvokeAsync(args);
        }
    }
}