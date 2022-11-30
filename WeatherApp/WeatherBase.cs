using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    /// <summary>
    /// Базовая структура данных которая возвращается с сервера
    /// https://www.accuweather.com/
    /// </summary>
    /// <param name="EffectiveDate"></param>
    /// <param name="EffectiveEpochDate"></param>
    /// <param name="Severity"></param>
    /// <param name="Text"></param>
    /// <param name="Category"></param>
    /// <param name="EndDate"></param>
    /// <param name="EndEpochDate"></param>
    /// <param name="MobileLink"></param>
    /// <param name="Link"></param>
    public record Headline(DateTime EffectiveDate, int EffectiveEpochDate, int Severity, string Text, string Category, DateTime EndDate, int EndEpochDate, string MobileLink, string Link);
    public record Maximum(double Value, string Unit, int UnitType);
    public record Minimum(double Value, string Unit, int UnitType);
    public record Temperature(Minimum Minimum, Maximum Maximum);
    public record Day(int Icon, string IconPhrase, bool HasPrecipitation, string PrecipitationType, string PrecipitationIntensity);
    public record Night(int Icon, string IconPhrase, bool HasPrecipitation, string PrecipitationType, string PrecipitationIntensity);
    public record DailyForecast(DateTime Date, int EpochDate, Temperature Temperature, Day Day, Night Night, List<string> Sources, string MobileLink, string Link);


    public record RootWeather(Headline Headline, List<DailyForecast> DailyForecasts);
}
