using IpLogger.Domain.Filters.Abstract;
using IpLogger.Domain.Helpers;
using IpLogger.Domain.Models;
using System.Net;

namespace IpLogger.Domain.Filters
{
    public class LogFilter : FilterBase<LogDTO>
    {
        public IPAddress? AddressStart { get; set; }
        public int Cidr { get; set; }
        public DateOnly TimeStart { get; set; }
        public DateOnly TimeEnd { get; set; }

        public override IEnumerable<Predicate<LogDTO>> GetPredicates()
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
