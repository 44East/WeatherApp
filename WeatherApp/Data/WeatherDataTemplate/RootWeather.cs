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
    public record RootWeather(Headline Headline, List<DailyForecast> DailyForecasts);
}
