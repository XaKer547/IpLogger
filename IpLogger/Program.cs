using IpLogger.Console.Commands;
using IpLogger.Console.Commands.Enums;
using IpLogger.Console.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;

namespace IpLogger.Console
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            try
            {
                var host = Hosting.Configuration.CreateHostBuilder(args)
               .Build();

                var command = host.Services.GetRequiredService<LogRootCommand>();

                await command.InvokeAsync(args);
            }
            catch { }

            return (int)ExitCodeManager.ExitCode;
        }
    }
}