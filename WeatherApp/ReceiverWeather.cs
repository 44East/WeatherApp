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
        private RootWeather rootWeather;
        public ReceiverWeather()
        {
            textMessages = new TextMessages();
            SearcherCity = new SearcherCity();
        }
        public SearcherCity SearcherCity { get; internal set; }
        public async void GetWeatherDataFromServer()
        {
            HttpClient httpClient = new HttpClient();
            var currCity = SearcherCity.GetCurrentCity();

            string apiKey = SearcherCity.ApiManager.userApiList?.FirstOrDefault().UserApiProperty;

            string jsonUrl = $"http://dataservice.accuweather.com/forecasts/v1/daily/5day/{currCity.Key}?apikey={apiKey}&language=ru&metric=true";

            using HttpResponseMessage responseMessage = httpClient.GetAsync(jsonUrl).Result;
            using HttpContent httpContent = responseMessage.Content;

            string receivedResult = await httpContent.ReadAsStringAsync();

            rootWeather = JsonSerializer.Deserialize<RootWeather>(receivedResult);

           
            foreach (var item in rootWeather.DailyForecasts)
            {
                Console.WriteLine(textMessages.PatternOfWeather, item.Date, item.Temperature.Minimum.Value,
                item.Temperature.Maximum.Value, item.Day.IconPhrase, item.Night.IconPhrase, currCity.LocalizedName);

            }


        }
    }
}
