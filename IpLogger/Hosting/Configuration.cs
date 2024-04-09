using IpLogger.Configuration;
using IpLogger.Services;
using IpLogger.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IpLogger.Console.Hosting
{
    public class Configuration
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                using var loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder
                        .AddFilter("Microsoft", LogLevel.Warning)
                        .AddFilter("System", LogLevel.Warning)
                        .AddFilter("NonHostConsoleApp.Program", LogLevel.Debug)
                        .AddConsole();
                });

                var logger = loggerFactory.CreateLogger("ProgramLogger");

                services.AddScoped(x => logger);

                services.AddScoped<ILogService, LogService>();

                services.AddTransient<ICommandConfiguration, LoggerCommandConfiguration>();
            });
    }
}
