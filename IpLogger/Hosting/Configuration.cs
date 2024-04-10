using IpLogger.Application.Services;
using IpLogger.Console.Commands;
using IpLogger.Console.Helpers;
using IpLogger.Console.Infrastucture;
using IpLogger.Console.Options;
using IpLogger.DataAccess.Data;
using IpLogger.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.IO.Abstractions;

namespace IpLogger.Console.Hosting
{
    public class Configuration
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder()
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

                services.AddScoped<IFileSystem, FileSystem>();

                services.AddScoped(provider =>
                {
                    var fileLog = OptionDefaults.FileLog;

                    fileLog.AddFileExistValidation();

                    var path = fileLog.Parse(args)
                    .GetValueForOption(OptionDefaults.FileLog)!;

                    var fileSystem = provider.GetRequiredService<IFileSystem>();

                    var logProvider = new LogProvider(path.FullName, fileSystem);

                    return logProvider;
                });

                services.AddScoped<ILogService, LogService>();

                services.AddScoped<LogWriter>();

                services.AddTransient<LogRootCommand>();
            });
    }
}