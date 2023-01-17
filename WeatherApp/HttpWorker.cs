

namespace WeatherApp
{
    /// <summary>
    /// Класс получения информации о погоде с сервера в строковом формате.
    /// </summary>
    public  class HttpWorker
    {
        private static HttpClient httpClient;
        private HttpWorker() { }
        static HttpWorker() => httpClient = new HttpClient();

        /// <summary>
        /// При использовании полноценного UI необходимо использовать асинхронный метод получения данных с сервера.
        /// Для отсутствия зависаний в интерфейсе, чтобы получение данных происходило во вториных потоках из пула.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> GetStringFromServerAsync(string url) => await httpClient.GetStringAsync(url);  
        /// <summary>
        /// Метод для консольного, построчного вывода данных с сервера, ввиду отсутствия полноценного UI у нас нет проблем с зависанием интерфейса.
        /// И метод может дожидаться данных в вызывающем потоке.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetStringFromServer(string url) => httpClient.GetStringAsync(url).Result;
        
        
    }
}
