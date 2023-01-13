using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public  class HttpWorker
    {
        
        /// <summary>
        /// При использовании полноценного UI необходимо использовать асинхронный метод получения данных с сервера.
        /// Для отсутствия зависаний в интерфейсе, чтобы получение данных происходило во вториных потоках из пула.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> GetStringFromServerAsync(string url)
        {
            HttpClient httpClient = new HttpClient();
            using (HttpResponseMessage responseMessage = await httpClient.GetAsync(url))
            {
                using (HttpContent httpContent = responseMessage.Content)
                {
                    return await httpContent.ReadAsStringAsync();
                }
            }
        }

        /// <summary>
        /// Метод для консольного, построчного вывода данных с сервера, ввиду отсутствия полноценного UI у нас нет проблем с зависанием интерфейса.
        /// И метод может дожидаться данных в вызывающем потоке.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetStringFromServer(string url) 
        {
            HttpClient httpClient = new HttpClient();
            using (HttpResponseMessage responseMessage = httpClient.GetAsync(url).Result)
            {
                using (HttpContent httpContent = responseMessage.Content)
                {
                    return httpContent.ReadAsStringAsync().Result;
                }
            }
        }
    }
}
