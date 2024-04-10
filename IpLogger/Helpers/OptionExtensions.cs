using IpLogger.Console.Options;
using System.CommandLine;

namespace IpLogger.Console.Helpers
{
    public static class OptionExtensions
    {
        public static Command AddIPAddressOptions(this Command command)
        {
            var addressStart = OptionDefaults.AddressStart;

            var addressMask = OptionDefaults.AddressMask;

            addressMask.AddOtherOptionRequiredValidation(addressStart);

            command.AddOption(addressStart);

            command.AddOption(addressMask);

            return command;
        }
        public static Command AddFileLogOption(this Command command)
        {
            var fileLog = OptionDefaults.FileLog;

            fileLog.AddFileExistValidation();

            command.AddOption(fileLog);

            return command;
        }
        public static Command AddFileOututOption(this Command command)
        {
            var fileOutput = OptionDefaults.FileOutput;

            fileOutput.AddCanCreateFileValidation();

            command.AddOption(fileOutput);

            return command;
        }
        public static Command AddTimeIntrevalOptions(this Command command)
        {
            var timeStart = OptionDefaults.TimeStart;

            var timeEnd = OptionDefaults.TimeEnd;

            command.AddOption(timeStart);

            command.AddOption(timeEnd);

            return command;
        }

        public static void AddCanCreateFileValidation(this Option<FileInfo> option)
        {
            option.AddValidator(result =>
            {
                FileInfo file = result.GetValueForOption(option)!;

                try
                {
                    FileHelper.CanCreateOrThrowException(file);
                }
                catch (Exception ex)
                {
                    ExitCodeManager.ExitCode |= Commands.Enums.ExitCodes.PathNotFound;

                    result.ErrorMessage = "Не удалось создать конечный файл\r\n";
                    result.ErrorMessage += ex.Message;
                }
            });

        }
        public static void AddFileExistValidation(this Option<FileInfo> option)
        {
            option.AddValidator(result =>
            {
                FileInfo file = result.GetValueForOption(option)!;

                try
                {
                    FileHelper.ExistsOrThrowException(file);
                }
                catch (Exception ex)
                {
                    result.ErrorMessage = "Файл с логами не найден или не существует\r\n";
                    result.ErrorMessage += ex.Message;

                    ExitCodeManager.ExitCode |= Commands.Enums.ExitCodes.FileNotFound;
                }
            });
        }
        public static void AddOtherOptionRequiredValidation(this Option option, Option requiredOption)
        {
            option.AddValidator(result =>
            {
                var address = result.GetValueForOption(requiredOption)!;

                if (address is null)
                {
                    result.ErrorMessage = $"Отсутсвует параметр {requiredOption.Name}";

                    ExitCodeManager.ExitCode |= Commands.Enums.ExitCodes.ArgumentError;
                }
            });
        }
    }
}
