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
        public async Task GetWeatherDataFromServer()
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
            string jsonUrl = $"http://dataservice.accuweather.com/forecasts/v1/daily/5day/{currCity.Key}?apikey={apiKey}&language=ru&metric=true";

            HttpClient httpClient = new HttpClient();
            using HttpResponseMessage responseMessage = httpClient.GetAsync(jsonUrl).Result;
            using HttpContent httpContent = responseMessage.Content;

            string receivedResult = await httpContent.ReadAsStringAsync();

            var rootWeather = JsonSerializer.Deserialize<RootWeather>(receivedResult);

            foreach (var item in rootWeather.DailyForecasts)
            {
                Console.WriteLine(textMessages.PatternOfWeather, item.Date, item.Temperature.Minimum.Value,
                item.Temperature.Maximum.Value, item.Day.IconPhrase, item.Night.IconPhrase, currCity.LocalizedName);

            }



        }
    }
}
