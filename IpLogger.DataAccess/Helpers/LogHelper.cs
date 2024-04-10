using IpLogger.DataAccess.Data.Entities.Constants;
using IpLogger.Models;
using System.Net;

namespace IpLogger.DataAccess.Helpers
{
    public static class LogHelper
    {
        public static bool TryParse(string value, out Log log)
        {
            log = null!;

            var values = value.Split(LogDefaults.Separator, 2);

            if (!IPAddress.TryParse(values[0], out var ipAddress))
                return false;

            if (!DateTime.TryParseExact(values[1], LogDefaults.DateTimeFormat, null, System.Globalization.DateTimeStyles.None, out var dateTime))
                return false;

            log = new Log
            {
                Address = ipAddress,
                LastAccessedTime = dateTime
            };

            return true;
        }
    }
}
