using IpLogger.Console.Commands.Enums;

namespace IpLogger.Console.Helpers
{
    public static class ExitCodeManager
    {
        public static ExitCodes ExitCode { get; set; } = ExitCodes.InvalidData;
    }
}
