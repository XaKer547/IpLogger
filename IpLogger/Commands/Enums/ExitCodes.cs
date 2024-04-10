namespace IpLogger.Console.Commands.Enums
{
    [Flags]
    public enum ExitCodes
    {
        Success = 0x0,
        FileNotFound = 0x1,
        PathNotFound = 0x2,
        ArgumentError = 0x4,
        InvalidData = 0x8,
    }
}