using System.Net;

namespace IpLogger.Helpers
{
    public static class IPAddressExtensions
    {
        public static bool IsInRange(this IPAddress address, IPAddress startIpAddr, IPAddress endIpAddr)
        {
            long ipStart = BitConverter.ToInt32(startIpAddr.GetAddressBytes().Reverse().ToArray(), 0);

            long ipEnd = BitConverter.ToInt32(endIpAddr.GetAddressBytes().Reverse().ToArray(), 0);

            long ip = BitConverter.ToInt32(address.GetAddressBytes().Reverse().ToArray(), 0);

            return ip >= ipStart && ip <= ipEnd;
        }
    }
}
