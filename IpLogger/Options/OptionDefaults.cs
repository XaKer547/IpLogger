using System.CommandLine;
using System.Net;

namespace IpLogger.Console.Options
{
    public static class OptionDefaults
    {
        public static readonly Option<IPAddress> AddressStart = new("--address-start", "Нижняя граница диапазона адресов, по умолчанию обрабатываются все адреса");

        public static readonly Option<int> AddressMask = new("--address-mask", "Маска подсети, задающая верхнюю границу диапазона десятичное число. " +
         "В случае, если он не указан, обрабатываются все адреса, начиная с нижней границы диапазона. Параметр нельзя использовать, если не задан address-start.");

        public static readonly Option<FileInfo> FileLog = new("--file-log", "Путь к файлу с логами")
        {
            IsRequired = true,
        };

        public static readonly Option<FileInfo> FileOutput = new("--file-output", "Путь к файлу с результатом")
        {
            IsRequired = true,
        };

        public static readonly Option<DateOnly> TimeStart = new("--time-start", "Нижняя граница временного интервала");

        public static readonly Option<DateOnly> TimeEnd = new("--time-end", "Верхняя граница временного интервала");
    }
}
