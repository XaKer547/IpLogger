using System.Net;

namespace IpLogger.Models
{
    public record Log(IPAddress Address, DateTime LastAccessedTime)
    {
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        private const string Separator = ":";

        public static bool TryParse(string value, out Log log)
        {
            log = null!;

            var values = value.Split(Separator, 2);

            if (!IPAddress.TryParse(values[0], out var ipAddress))
                return false;

            if (!DateTime.TryParseExact(values[1], DateTimeFormat, null, System.Globalization.DateTimeStyles.None, out var dateTime))
                return false;

            log = new Log(ipAddress, dateTime);

            return true;
        }

        public override string ToString()
        {
            return $"{Address}:{LastAccessedTime.ToString(DateTimeFormat)}";
        }
    }
}
