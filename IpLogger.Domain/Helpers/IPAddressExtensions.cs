using System.Net;

namespace IpLogger.Domain.Helpers
{
    public static class IPAddressExtensions
    {
        public static bool IsInRange(this IPAddress ipAddress, IPAddress CIDRAddress, int CIDR)
        {
            var IP_addr = BitConverter.ToInt32(ipAddress.GetAddressBytes(), 0);

            var CIDR_addr = BitConverter.ToInt32(CIDRAddress.GetAddressBytes(), 0);

            var CIDR_mask = IPAddress.HostToNetworkOrder(-1 << 32 - CIDR);

            return (IP_addr & CIDR_mask) == (CIDR_addr & CIDR_mask);
        }

        public static bool IsBigger(this IPAddress address, IPAddress startIpAddr)
        {
            long ipStart = BitConverter.ToInt32(startIpAddr.GetAddressBytes().Reverse().ToArray(), 0);

            long ip = BitConverter.ToInt32(address.GetAddressBytes().Reverse().ToArray(), 0);

            return ip >= ipStart;
        }
    }
}
