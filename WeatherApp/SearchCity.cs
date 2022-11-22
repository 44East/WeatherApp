using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class SearchCity
    {
        private UserApiManager apiManager;
        public void GettingListOfCitesOnRequest(string cityName)
        {
            string apiKey = apiManager.userApiList.FirstOrDefault()?.UserApiProperty;

            try
            {
                string jsonOnWeb = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={apiKey}&q={cityName}";
                HttpClient httpClient = new HttpClient();
                string prepareString = httpClient.Downlo
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
    }
}
