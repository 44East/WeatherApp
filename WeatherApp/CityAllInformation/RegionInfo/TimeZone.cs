using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.CityAllInformation
{
    public record TimeZone(string Code, string Name, int GmtOffset, bool IsDaylightSaing, object NextOffsetChange);
}
