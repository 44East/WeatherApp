using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public record TimeZone(string Code, string Name, double GmtOffset, bool IsDaylightSaing, object NextOffsetChange);
}
