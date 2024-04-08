namespace IpLogger.Console.Helpers
{
    public static class ConsoleHelper
    {
        public static void WriteLineError(string error)
        {
            SetConsoleErrorForeground();

            System.Console.Error.WriteLine(error);

            ResetConsoleColor();
        }

        public static void WriteLineErrors(string[] errors)
        {
            SetConsoleErrorForeground();

            foreach (var error in errors)
                System.Console.Error.WriteLine(error);

            ResetConsoleColor();
        }

        private static void SetConsoleErrorForeground() => System.Console.ForegroundColor = ConsoleColor.Red;

        private static void ResetConsoleColor() => System.Console.ResetColor();
    }
}
