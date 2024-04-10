using System.IO.Abstractions.TestingHelpers;

namespace IpLogger.Tests.Helpers
{
    internal static class MockDataHelper
    {
        public static MockFileData GetMockData()
        {
            var logs = GetLogs();

            var logsString = string.Join("\r\n", logs);

            var mockFileData = new MockFileData(logsString);

            return mockFileData;
        }

        private static string[] GetLogs()
        {
            var logs = new string[]
            {
                "75.183.39.40:2024-04-04 11:39:00",
                "152.118.18.252:2024-04-04 08:31:00",
                "35.17.86.195:2024-04-04 05:30:00",
                "29.107.13.250:2024-04-03 10:29:00",
                "123.51.62.143:2024-04-03 09:44:00",
                "60.90.252.154:2024-04-03 09:43:00",
                "27.108.37.140:2024-04-03 01:28:00",
                "102.83.30.63:2024-04-02 00:35:00",
                "19.228.65.96:2024-04-01 11:34:00",
            };

            return logs;
        }
    }
}
