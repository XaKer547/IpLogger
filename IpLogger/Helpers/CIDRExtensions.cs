using System.Net;

namespace IpLogger.Helpers
{
    public class CIDRExtensions
    {
        public static IPAddress CidrToMask(int cidr)
        {
            var mask = (cidr == 0) ? 0 : uint.MaxValue << (32 - cidr);

            var bytes = BitConverter.GetBytes(mask).Reverse().ToArray();

            return new IPAddress(bytes);
        }
    }
}
