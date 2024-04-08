using IpLogger.Commands;
using IpLogger.Configuration;
using IpLogger.Services;
using IpLogger.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.CommandLine.Builder;
using System.CommandLine.IO;
using System.CommandLine.Parsing;

namespace IpLogger
{
    internal class Program
    {
        static void Main(string[] args)
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

            var services = new ServiceCollection();

            services.AddScoped(x => logger);

            services.AddScoped<ILogService, LogService>();

            services.AddTransient<ICommandConfiguration, LoggerCommandConfiguration>();

            services.AddTransient(provider =>
            {
                var configuration = provider.GetRequiredService<ICommandConfiguration>();

                return new LoggerCommand(args, configuration);
            });

            var serviceProvider = services.BuildServiceProvider();

            var command = serviceProvider.GetRequiredService<LoggerCommand>();

            command.Invoke();
            //var parser = new CommandLineBuilder(command.ConfigureRootCommand())
            //    .UseParseDirective(0xA0)
            //    .UseHelp()
            //    .UseSuggestDirective()
            //    .UseParseErrorReporting(0XA0)
            //    .UseExceptionHandler((exception, context) =>
            //    {
            //        if (exception is OperationCanceledException)
            //            return;

            //        //    context.Console.ResetTerminalForegroundColor();
            //        //    context.Console.SetTerminalForegroundRed();

            //        context.Console.Error.Write(context.LocalizationResources.ExceptionHandlerHeader());
            //        context.Console.Error.WriteLine(exception.ToString());

            //        //context.Console.ResetTerminalForegroundColor();

            //        context.ExitCode = 0xA0;
            //    }, 0xA0);

            //var a = parser.Build();

            //a.Invoke(args);
        }

        private Parser a()
        {
            var parser = new CommandLineBuilder()
                .UseParseDirective()
                .UseHelp()
                .UseSuggestDirective()
                .UseParseErrorReporting()
                .UseExceptionHandler((exception, context) =>
                {
                    if (exception is OperationCanceledException)
                        return;

                    //    context.Console.ResetTerminalForegroundColor();
                    //    context.Console.SetTerminalForegroundRed();

                    context.Console.Error.Write(context.LocalizationResources.ExceptionHandlerHeader());
                    context.Console.Error.WriteLine(exception.ToString());

                    //context.Console.ResetTerminalForegroundColor();

                    context.ExitCode = 0xA0; ;
                });

            return parser.Build();
        }
    }
}