using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public record Headline
    (
        DateTime EffectiveDate, 
        int EffectiveEpochDate, 
        int Severity, 
        string Text, 
        string Category, 
        DateTime EndDate, 
        int EndEpochDate, 
        string MobileLink, 
        string Link
        );

}
