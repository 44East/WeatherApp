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

        public UserApiManager ApiManager { get; }
        public DataRepo DataRepo { get; private set; }

        private TextMessages textMessages;
        private TextWorker textWorker;
        public SearcherCity(TextMessages textMessages, TextWorker textWorker)
        {
            this.textMessages = textMessages;
            this.textWorker = textWorker;
            ApiManager = new UserApiManager(textMessages, textWorker);
            DataRepo = new DataRepo(textMessages,textWorker);
            
        }

        /// <summary>
        /// Метод принимает две строковые переменные, [1.название города кирилицей или латиницей] и [2.язык поискового запроса]
        /// Возвращает массив данных из одного или нескольких городов, в зависимости от совпадений на сервере
        /// </summary>
        /// <param name="cityName"></param>
        /// <param name="searchLanguage"></param>
        public void GettingListOfCitesOnRequest(HttpWorker httpWorker, string cityName, string searchLanguage)
        {
            StringBuilder fullUrlToRequest = new StringBuilder();   
            try
            {
                string apiKey = ApiManager.UserApiList.FirstOrDefault().UserApiProperty;
                fullUrlToRequest.AppendFormat(textMessages.SearchCityUrl, apiKey, cityName, searchLanguage);
                string prepareString = httpWorker.GetStringFromServer(fullUrlToRequest.ToString());

                List<RootBasicCityInfo> rbci = JsonSerializer.Deserialize<List<RootBasicCityInfo>>(prepareString);
                if (rbci.Count == 0)
                    throw new JsonException(textMessages.SearchError);

                DataRepo.ShowReceivedCities(rbci);
            }
            catch(NullReferenceException ex)
            {
                textWorker.ShowTheText(textMessages.ApiIsEmpty);
                textWorker.ShowTheText(ex.Message);
            }
            catch (AggregateException ex)
            {
                textWorker.ShowTheText(textMessages.NetworkOrHostIsNotAwailable);
                textWorker.ShowTheText(ex.Message);
            }
            catch (JsonException ex)
            {
                textWorker.ShowTheText(textMessages.ErorrsBySearch + ex.Message);
            }

        }
        /// <summary>
        /// Удаляет город из списка через экземпляр DataRepo 
        /// </summary>
        public void RemoveCityFromList()
        {
            try
            {
                DataRepo.RemoveCityFromSavedList(GetCurrentCity());
            }
            catch(ArgumentNullException ex)
            {
                textWorker.ShowTheText(ex.Message);
            }
        }
        /// <summary>
        /// Возвращает выбранный пользователем экземпляр города из коллекции, для манипуляций с ним(удаление или вывод погоды)
        /// </summary>
        /// <returns></returns>
        public RootBasicCityInfo GetCurrentCity()
        {
            if (DataRepo.ListOfCitiesForMonitoringWeather.Count < 1)
                throw new ArgumentNullException(textMessages.CityListIsEmpty);

            textWorker.ShowSavedCity(DataRepo.ListOfCitiesForMonitoringWeather);
            bool isCityNumExist;

            int cityNum;
            Console.Write(textMessages.GetCityNum);
            do
            {
                isCityNumExist = true;
                while (!int.TryParse(Console.ReadLine(), out cityNum))
                {
                    textWorker.ShowTheText(textMessages.IntParseError + textMessages.GetCityNum);
                }                

                if (cityNum < 1 || cityNum > DataRepo.ListOfCitiesForMonitoringWeather.Count)
                {
                    textWorker.ShowTheText(textMessages.CityNoExist);
                    isCityNumExist = false;
                }
            }
            while (!isCityNumExist);
            return DataRepo.ListOfCitiesForMonitoringWeather[cityNum - 1];
        }
    }
}
