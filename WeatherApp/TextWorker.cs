using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace WeatherApp
{
    public class TextWorker
    {
        public delegate void ShowTextHandler(string text);
        public event ShowTextHandler ShowText;


        private TextMessages textMessages;
        public TextWorker(TextMessages textMessages)
        {
             this.textMessages = textMessages;
        }

        /// <summary>
        /// Выводит текстовые сообщения, метод для присвоения делегату, сигнатура может быть переназначена, например для вывода в TextBox(WPF)
        /// </summary>
        /// <param name="text"></param>
        public static void OutputText(string text) => Console.WriteLine(text);
        public void ShowTheText(string text)
        {
            ShowText?.Invoke(text);
        }
        public void ShowWeatherInCurrentCity(RootBasicCityInfo currentCity, string receivedWeatherForCurrentCity)
        {
                var rootWeather = JsonSerializer.Deserialize<RootWeather>(receivedWeatherForCurrentCity);
                foreach (var item in rootWeather.DailyForecasts)
                {
                    Console.ForegroundColor = item.Temperature.Minimum.Value < 0.0d && item.Temperature.Maximum.Value < 0.0d ? ConsoleColor.DarkBlue : item.Temperature.Minimum.Value < 0.0d ? ConsoleColor.Cyan : ConsoleColor.Yellow;
                    Console.WriteLine(textMessages.PatternOfWeather, item.Date, item.Temperature.Minimum.Value,
                    item.Temperature.Maximum.Value, item.Day.IconPhrase, item.Night.IconPhrase, currentCity.LocalizedName);
                    Console.ResetColor();

                }
        }
        public void ShowSavedCity(List<RootBasicCityInfo> cityList) 
        {
            foreach (var item in cityList ?? throw new NullReferenceException())
            {
                Console.WriteLine(textMessages.PatternOfCity, cityList.IndexOf(item) + 1,
                item.EnglishName, item.LocalizedName, item.Country.LoacalizedName,
                item.AdministrativeArea.LocalizedName, item.AdministrativeArea.LocalizedType);

            }
        }
    }
}
