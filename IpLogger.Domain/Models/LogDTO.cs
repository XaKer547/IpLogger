using System.Net;

namespace IpLogger.Domain.Models
{
    public record LogDTO(IPAddress Address, DateTime LastAccessedTime)
    {
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        private const string Separator = ":";

        public override string ToString()
        {
            return $"{Address}:{LastAccessedTime.ToString(DateTimeFormat)}";
        }
    }
}
