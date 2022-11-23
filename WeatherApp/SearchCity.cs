using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class SearchCity
    {

        private UserApiManager apiManager;
        public async Task GettingListOfCitesOnRequest(string cityName)
        {
            string apiKey = apiManager.userApiList.FirstOrDefault()?.UserApiProperty;

            try
            {
                string jsonOnWeb = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={apiKey}&q={cityName}";
                HttpClient httpClient = new HttpClient();
                using(HttpResponseMessage response = httpClient.GetAsync(jsonOnWeb).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        var prepareString = content.ReadAsStringAsync();
                        var result = await Task.FromResult(prepareString);
                        ObservableCollection<RootBasicCityInfo> rbci = JsonSerializer.DeserializeAsync<ObservableCollection<RootBasicCityInfo>>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
    }
}
