using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public record Day(int Icon, string IconPhrase, bool HasPrecipitation, string PrecipitationType, string PrecipitationIntensity);
}
