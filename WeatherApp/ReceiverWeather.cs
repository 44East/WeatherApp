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
        public SearcherCity SearcherCity { get; }
        private TextMessages textMessages;
        private TextWorker textWorker;
        public ReceiverWeather(TextMessages textMessages, TextWorker textWorker)
        {
            this.textMessages = textMessages;            
            this.textWorker = textWorker;
            SearcherCity = new SearcherCity(textMessages, textWorker);
        }        
        /// <summary>
        /// Метод запрашивает API ключ доступа к серверу и уникальный номер сохраненного города, если пара ключ номер приняты сервером
        /// Выводит погоду на 5 дней по выбранному городу
        /// Если список городов пуст или API ключ недоступен, выводится соответствующее сообщение по каждому событию и происходит выход из метода
        /// </summary>
        public void GetWeatherDataFromServer(HttpWorker httpWorker)
        {            
            RootBasicCityInfo currentCity;
            string receivedWeatherForCurrentCity;
            StringBuilder fullUrlToRequest = new StringBuilder();            
            try 
            {
                currentCity = SearcherCity.GetCurrentCity();                
                string apiKey = SearcherCity.ApiManager.UserApiList?.FirstOrDefault().UserApiProperty;

                fullUrlToRequest.AppendFormat(textMessages.GetWeatherUrl, currentCity.Key, apiKey);

                receivedWeatherForCurrentCity = httpWorker.GetStringFromServer(fullUrlToRequest.ToString());
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
            textWorker.ShowWeatherInCurrentCity(currentCity, receivedWeatherForCurrentCity);                  

        }
        
    }
}
