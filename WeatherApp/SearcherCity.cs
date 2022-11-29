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
        public SearcherCity()
        {
            ApiManager= new UserApiManager();
            DataRepo= new DataRepo();
        }

        
        public async void GettingListOfCitesOnRequest(string cityName, string searchLanguage)
        {
            string apiKey = ApiManager.userApiList.FirstOrDefault().UserApiProperty;

            try
            {
                string jsonOnWeb = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={apiKey}&q={cityName}&language={searchLanguage}";
                HttpClient httpClient = new HttpClient();
                
                using HttpResponseMessage response = httpClient.GetAsync(jsonOnWeb).Result;
                using HttpContent content = response.Content;
                
                string prepareString = await content.ReadAsStringAsync();

                
                ObservableCollection<RootBasicCityInfo> rbci = JsonSerializer.Deserialize<ObservableCollection<RootBasicCityInfo>>(prepareString);

                DataRepo.PrintReceivedCities(rbci);



            }
            catch (Exception ex)
            {
                Console.WriteLine("Неполучилось отобразить запрашиваемый город."
                + "Возможные причины: \n" +
                "* Неправильно указано название города\n"
                + "* Нет доступа к интернету\n"
                + "Подробнее ниже: \n"
                + ex.Message);

            }
        }
    }
}
