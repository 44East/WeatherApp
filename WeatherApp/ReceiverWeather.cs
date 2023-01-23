using System.Text;
using System.Text.Json;


namespace WeatherApp
{
    public class ReceiverWeather
    {
        private SearcherCity searcherCity;
        private UserApiManager apiManager;
        private TextMessages textMessages;
        private TextWorker textWorker;
        public ReceiverWeather(TextMessages textMessages, TextWorker textWorker, SearcherCity searcherCity, UserApiManager apiManager)
        {
            this.textMessages = textMessages;            
            this.textWorker = textWorker;
            this.searcherCity = searcherCity;
            this.apiManager = apiManager;
        }        
        /// <summary>
        /// Метод запрашивает API ключ доступа к серверу и уникальный номер сохраненного города, если пара (ключ/номер) приняты сервером
        /// то метод выводит погоду на 5 дней по выбранному городу.
        /// Если список городов пуст или API ключ недоступен, выводится соответствующее сообщение по каждому событию и происходит выход из метода.
        /// </summary>
        public void GetWeatherDataFromServer()
        {            
            RootBasicCityInfo currentCity;
            string receivedWeatherForCurrentCity;
            StringBuilder fullUrlToRequest = new StringBuilder();
            RootWeather rootWeather;
            try 
            {
                currentCity = searcherCity.GetCurrentCity();
                string apiKey = apiManager.GetTheFirstKey();

                fullUrlToRequest.AppendFormat(textMessages.GetWeatherUrl, currentCity.Key, apiKey);

                receivedWeatherForCurrentCity = HttpWorker.GetStringFromServer(fullUrlToRequest.ToString());
                
                rootWeather = JsonSerializer.Deserialize<RootWeather>(receivedWeatherForCurrentCity);

                textWorker.ShowWeatherInCurrentCity(currentCity, rootWeather);
            }
            catch(ArgumentNullException ex)
            {
                textWorker.ShowTheText(ex.Message);
                return;
            }
            catch(NullReferenceException ex)
            {
                textWorker.ShowTheText(textMessages.ApiIsEmpty);
                textWorker.ShowTheText(ex.Message);
                return;
            }
            catch (AggregateException ex)
            {
                textWorker.ShowTheText(textMessages.NetworkOrHostIsNotAwailable);
                textWorker.ShowTheText(ex.Message);
                return;
            }
            catch(JsonException ex)
            {
                textWorker.ShowTheText(textMessages.ReceiveWeatherError);
                textWorker.ShowTheText(ex.Message);
            }
                             

        }
        
    }
}
