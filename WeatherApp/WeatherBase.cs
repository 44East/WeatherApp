using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public record Headline(DateTime EffectiveDate, int EffectiveEpochDate, int Severity, string Text, string Category, DateTime EndDate, int EndEpochDate, string MobileLink, string Link);
    public record Maximum(double Value, string Unit, int UnitType);
    public record Minimum(double Value, string Unit, int UnitType);
    public record Temperature(Minimum Minimum, Maximum Maximum);
    public record Day(int Icon, string IconPhrase, bool HasPrecipitation, string PrecipitationType, string PrecipitationIntensity);
    public record Night(int Icon, string IconPhrase, bool HasPrecipitation, string PrecipitationType, string PrecipitationIntensity);
    public record DailyForecast(DateTime Date, int EpochDate, Temperature Temperature, Day Day, Night Night, List<string> Sources, string MobileLink, string Link);


    public record RootWeather(Headline Headline, List<DailyForecast> DailyForecasts);
}
