using IpLogger.Console.Options;
using System.CommandLine;

namespace IpLogger.Console.Helpers
{
    public static class OptionExtensions
    {
        public static RootCommand AddIPAddressOption(this RootCommand command)
        {
            var addressStart = OptionDefaults.AddressStart;

            var addressMask = OptionDefaults.AddressMask;

            addressMask.AddOtherOptionValidation(addressStart);

            addressMask.AddValidator(result =>
            {
                var address = result.GetValueForOption(addressStart)!;

                if (address is null)
                    result.ErrorMessage = "Отсутсвует параметр --address-start";
            });

            command.AddOption(addressStart);

            command.AddOption(addressMask);

            return command;
        }
        public static RootCommand AddFileLogOption(this RootCommand command)
        {
            var fileLog = OptionDefaults.FileLog;

            fileLog.AddFileExistValidation();

            command.AddOption(fileLog);

            return command;
        }
        public static RootCommand AddFileOututOption(this RootCommand command)
        {
            var fileOutput = OptionDefaults.FileOutput;

            fileOutput.AddCanCreateFileValidation();

            command.AddOption(fileOutput);

            return command;
        }
        public static RootCommand AddTimeIntrevalOptions(this RootCommand command)
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
                    result.ErrorMessage = "Не удалось создать конечный файл\n\r";
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
                    result.ErrorMessage = "Файл с логами не найден или не существует\n\r";
                    result.ErrorMessage += ex.Message;
                }
            });
        }
        public static void AddOtherOptionValidation(this Option option, Option requiredOption)
        {
            option.AddValidator(result =>
            {
                var address = result.GetValueForOption(requiredOption)!;

                if (address is null)
                    result.ErrorMessage = "Отсутсвует параметр --address-start";
            });
        }
    }
}
