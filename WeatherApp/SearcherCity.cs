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
        /// Метод принимает две строковые переменные, [1.название города кирилицей или латиницей] и [2.язык поискового запроса]
        /// Возвращает массив данных из одного или нескольких городов, в зависимости от совпадений на сервере
        /// </summary>
        /// <param name="cityName"></param>
        /// <param name="searchLanguage"></param>
        public void GettingListOfCitesOnRequest(HttpWorker httpWorker, string cityName, string searchLanguage)
        {
            string apiKey = ApiManager.userApiList.FirstOrDefault().UserApiProperty;
            try
            {                
                string prepareString = httpWorker.GetStringFromServer(string.Format(textMessages.SearchCityUrl,apiKey, cityName, searchLanguage));
                
                List<RootBasicCityInfo> rbci = JsonSerializer.Deserialize<List<RootBasicCityInfo>>(prepareString);

                DataRepo.PrintReceivedCities(rbci);
            }
            catch (JsonException ex)
            {
                Console.WriteLine(textMessages.ErorrsBySearch + ex.Message);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
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
