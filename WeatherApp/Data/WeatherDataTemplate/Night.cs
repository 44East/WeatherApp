using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public record Night(int Icon, string IconPhrase, bool HasPrecipitation, string PrecipitationType, string PrecipitationIntensity);
}
