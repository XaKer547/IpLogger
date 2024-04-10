using System.Net;

namespace IpLogger.Models
{
    public class Log
    {
        public IPAddress Address { get; set; }
        public DateTime LastAccessedTime { get; set; }
    }
}
