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
        public ReceiverWeather()
        {
            SearcherCity = new SearcherCity();
        }
        public SearcherCity SearcherCity { get; internal set; }
        public async void GetWeatherDataFromServer()
        {
            HttpClient httpClient = new HttpClient();

            string pattern = "=====\n" + "Номер в списке: {0}\n" + "Название в оригинале: {1}\n"
            + "В переводе:  {2} \n" + "Страна: {3}\n" + "Административный округ: {4}\n"
            + "Тип: {5}\n" + "====\n";
            int numberInList = 0;

            foreach (var item in  SearcherCity.DataRepo.ListOfCitiesForMonitoringWeather)
            {
                Console.WriteLine(pattern, numberInList.ToString(),
                item.EnglishName, item.LocalizedName, item.Country.LoacalizedName,
                item.AdministrativeArea.LocalizedName, item.AdministrativeArea.LocalizedType);
                numberInList++;

            }

            bool ifNotExist = false;
            string cityKey = null;
            int cityNum;
            Console.Write("Номер города для просмотра: ");
            while (!int.TryParse(Console.ReadLine(), out cityNum))
            {
                Console.WriteLine("Ввод должен содержать только числовой набор символов!\nНомер города для просмотра: ");
            }
            do
            {
                
                ifNotExist = default;
                
                
                if (cityNum < 0|| cityNum > SearcherCity.DataRepo.ListOfCitiesForMonitoringWeather.Count - 1)
                {
                    Console.WriteLine("Такого номера нет. Попробуйте еще раз.");
                    ifNotExist = true;  
                }
            }
            while (ifNotExist);

            cityKey = SearcherCity.DataRepo.ListOfCitiesForMonitoringWeather[cityNum].Key;

            string apiKey = SearcherCity.ApiManager.userApiList?.FirstOrDefault().UserApiProperty;

            string jsonUrl = $"http://dataservice.accuweather.com/forecasts/v1/daily/5day/{cityKey}?apikey={apiKey}&language=ru&metric=true";

            using HttpResponseMessage responseMessage = httpClient.GetAsync(jsonUrl).Result;
            using HttpContent httpContent = responseMessage.Content;

            string receivedResult = await httpContent.ReadAsStringAsync();

            RootWeather rootWeather = JsonSerializer.Deserialize<RootWeather>(receivedResult);
            
            string patternWeather = "=====\n" + "Дата: {0}\n" + "Температура минимальная: {1}\n"
            + "Температура максимальная: {2}\n" + "Примечание на день: {3}\n" + "Примечание на ночь: {4}\n" + "====\n";


            foreach (var item in rootWeather.DailyForecasts)
            {
                Console.WriteLine(patternWeather, item.Date, item.Temperature.Minimum.Value,
                item.Temperature.Maximum.Value, item.Day.IconPhrase, item.Night.IconPhrase);

            }


        }
    }
}
