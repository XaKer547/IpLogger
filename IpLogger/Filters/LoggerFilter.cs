using IpLogger.Console.Filters.Abstract;
using IpLogger.Helpers;
using IpLogger.Models;
using System.Net;

namespace IpLogger.Console.Filters
{
    public class LoggerFilter : FilterBase<Log>
    {
        public IPAddress? AddressStart { get; set; }
        public int Cidr { get; set; }
        public DateOnly TimeStart { get; set; }
        public DateOnly TimeEnd { get; set; }

        public override IEnumerable<Predicate<Log>> GetPredicates()
        {
            if (AddressStart is not null)
            {
                if (Cidr != default)
                    yield return l => l.Address.IsInRange(AddressStart, Cidr);
                else
                    yield return a => a.Address.IsBigger(AddressStart);
            }

            if (TimeStart != default)
                yield return l => DateOnly.FromDateTime(l.LastAccessedTime) > TimeStart;

            if (TimeEnd != default)
                yield return l => DateOnly.FromDateTime(l.LastAccessedTime) < TimeEnd;
        }
    }
}
