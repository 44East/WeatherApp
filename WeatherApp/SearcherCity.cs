using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class SearcherCity
    {

        public UserApiManager ApiManager { get; private set; }
        public DataRepo DataRepo {get; private set;}
        private TextMessages textMessages;
        public SearcherCity()
        {
            textMessages= new TextMessages();
            ApiManager= new UserApiManager();
            DataRepo= new DataRepo();
        }

        /// <summary>
        /// Метод принимает две строковые переменные, название города кирилицей или латиницей и язык поискового запроса
        /// Возвращает массив данных из одного или нескольких городов, в зависимости от совпадений на сервере
        /// </summary>
        /// <param name="cityName"></param>
        /// <param name="searchLanguage"></param>
        public async Task GettingListOfCitesOnRequestAsync(HttpClient httpClient,string cityName, string searchLanguage)
        {
            string apiKey = ApiManager.userApiList.FirstOrDefault().UserApiProperty;
            try
            {
                string jsonOnWeb = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={apiKey}&q={cityName}&language={searchLanguage}";
                // Ввиду построчного вывода в консоли, дабы не терялся порядок выода сообщений мы ждем ответа от сервера с помощью свойства Result,
                // Если бы мы использовали полноценный UI, для отсутствия зависаний в интерфейсе, 
                // Нам бы следовало использовать следующую конструкцию, ниже:
                // using HttpResponseMessage response = await httpClient.GetAsync(jsonOnWeb);
                using HttpResponseMessage response = httpClient.GetAsync(jsonOnWeb).Result;
                using HttpContent content = response.Content;
                
                string prepareString = await content.ReadAsStringAsync();
                
                List<RootBasicCityInfo> rbci = JsonSerializer.Deserialize<List<RootBasicCityInfo>>(prepareString);

                DataRepo.PrintReceivedCities(rbci);
            }
            catch (Exception ex)
            {
                Console.WriteLine(textMessages.ErorrsBySearch + ex.Message);
            }
        }
        /// <summary>
        /// Удаляет город из списка через экземпляр DataRepo 
        /// </summary>
        public void RemoveCityFromList() => DataRepo.RemoveCityFromSavedList(GetCurrentCity());
        /// <summary>
        /// Возвращает выбранный пользователем экземпляр города из коллекции, для манипуляций с ним(удаление или вывод погоды)
        /// </summary>
        /// <returns></returns>
        public RootBasicCityInfo GetCurrentCity()
        {
            if (DataRepo.ListOfCitiesForMonitoringWeather.Count < 1)
                return null;
            foreach (var item in DataRepo.ListOfCitiesForMonitoringWeather)
            {
                Console.WriteLine(textMessages.PatternOfCity, DataRepo.ListOfCitiesForMonitoringWeather.IndexOf(item) + 1,
                item.EnglishName, item.LocalizedName, item.Country.LoacalizedName,
                item.AdministrativeArea.LocalizedName, item.AdministrativeArea.LocalizedType);

            }

            bool ifNotExist = false;

            int cityNum;
            Console.Write(textMessages.GetCityNum);
            do
            {
                while (!int.TryParse(Console.ReadLine(), out cityNum))
                {
                    Console.WriteLine(textMessages.IntParseError + textMessages.GetCityNum);
                }


                ifNotExist = default;

                if (cityNum < 1 || cityNum > DataRepo.ListOfCitiesForMonitoringWeather.Count)
                {
                    Console.WriteLine(textMessages.CityNoExist);
                    ifNotExist = true;
                }
            }
            while (ifNotExist);
            return DataRepo.ListOfCitiesForMonitoringWeather[cityNum - 1];
        }
    }
}
