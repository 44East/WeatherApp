using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;



namespace WeatherApp
{
    public class ReceiverWeather
    {
        private TextMessages textMessages;

        public ReceiverWeather()
        {
            textMessages = new TextMessages();
            SearcherCity = new SearcherCity();
        }
        public SearcherCity SearcherCity { get; internal set; }
        /// <summary>
        /// Метод запрашивает API ключ доступа к серверу и уникальный номер сохраненного города, если пара ключ номер приняты сервером
        /// Выводит погоду на 5 дней по выбранному городу
        /// Если список городов пуст или API ключ недоступен, выводится соответствующее сообщение по каждому событию и происходит выход из метода
        /// </summary>
        public void GetWeatherDataFromServer(HttpWorker httpWorker)
        {
            var currCity = SearcherCity.GetCurrentCity();
            string apiKey = SearcherCity.ApiManager.userApiList?.FirstOrDefault().UserApiProperty;
            if (currCity == null)
            {
                Console.WriteLine(textMessages.ListIsEmpty);
                return;
            }
            if (string.IsNullOrEmpty(apiKey))
            {
                Console.WriteLine(textMessages.ApiIsEmpty);
                return;
            }
            string receivedResult = null;
            try
            {
                //receivedResult = httpWorker.GetStringFromServer(string.Format(textMessages.GetWeatherUrl, currCity.Key, apiKey));
                receivedResult = httpWorker.GetStringFromServer(string.Format(textMessages.GetWeatherUrl, SearcherCity.GetCurrentCity().Key, SearcherCity.ApiManager.userApiList?.FirstOrDefault().UserApiProperty));
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.Message);
            }

            RootWeather rootWeather = null;
            try
            {
                rootWeather = JsonSerializer.Deserialize<RootWeather>(receivedResult);
                foreach (var item in rootWeather.DailyForecasts)
                {
                    Console.ForegroundColor = item.Temperature.Minimum.Value < 0.0d && item.Temperature.Maximum.Value < 0.0d ? ConsoleColor.DarkBlue : item.Temperature.Minimum.Value < 0.0d ? ConsoleColor.Cyan : ConsoleColor.Yellow;
                    Console.WriteLine(textMessages.PatternOfWeather, item.Date, item.Temperature.Minimum.Value,
                    item.Temperature.Maximum.Value, item.Day.IconPhrase, item.Night.IconPhrase, currCity.LocalizedName);
                    Console.ResetColor();

                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (JsonException ex)
            {
                Console.WriteLine(ex.Message);
            }

            



        }
    }
}
