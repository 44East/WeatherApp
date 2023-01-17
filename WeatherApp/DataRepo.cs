using System.Text.Json;

namespace WeatherApp
{
    public class DataRepo
    {
        private TextMessages textMessages;
        private TextWorker textWorker;
        private FileWorker<RootBasicCityInfo> fileWorker;
        public DataRepo(TextMessages textMessages,TextWorker textWorker)
        {
            fileWorker = new FileWorker<RootBasicCityInfo>();
            ListOfCitiesForMonitoringWeather = new List<RootBasicCityInfo>();
            this.textMessages = textMessages;
            this.textWorker = textWorker;
        }
        /// <summary>
        /// Временно хранит прочитанные города из файла с локального диска, для дальнейшего вывода по ним погоды. 
        /// </summary>
        public List<RootBasicCityInfo> ListOfCitiesForMonitoringWeather { get; private set; }   
        /// <summary>
        /// При запуске читает локальнй файл сохраненных городов и записывает их в коллекцию, если файл еще не создан или
        /// удален/перемещен, то экземпляр файлового обработчика создает пустой файл и пробрасывает соответствующее исключение.
        /// </summary>
        public void ReadListOfCityMonitoring()
        {   
            try
            {
                ListOfCitiesForMonitoringWeather = fileWorker.ReadFileFromLocalDisk(textMessages.CityFileName);
            }
            catch(JsonException ex)
            {
                textWorker.ShowTheText(textMessages.CityFileFailure);
            }
            catch(FileNotFoundException ex)
            {
                textWorker.ShowTheText(textMessages.CityFileDoesntExist);
                textWorker.ShowTheText(ex.Message);
                
            }
            catch(Exception ex)
            {
                textWorker.ShowTheText(ex.Message);
            }          
            
        }        
        /// <summary>
        /// Записывает в файл все изменения такие как добавление нового города или удаление города из списка.
        /// </summary>
        private void WriteListOfCityMonitoring()
        {
            try
            {
                fileWorker.WriteFileToLocalStorage(ListOfCitiesForMonitoringWeather, textMessages.CityFileName);
            }
            catch(JsonException ex)
            {
                textWorker.ShowTheText(textMessages.CityListWritingFailure);
                textWorker.ShowTheText(ex.Message);
            }
            catch(Exception ex)
            {
                textWorker.ShowTheText(ex.Message);
            }
        }
        /// <summary>
        /// Удаляет выбранный пользователем экземпляр города из коллекции и записывает изменения в файл.
        /// </summary>
        /// <param name="rootBasicCityInfo"></param>
        public void RemoveCityFromSavedList(RootBasicCityInfo rootBasicCityInfo)
        {   
            ListOfCitiesForMonitoringWeather.Remove(rootBasicCityInfo);
            WriteListOfCityMonitoring();
        }
        /// <summary>
        /// Принимает временную коллекцию городов которую вернул поиск с сервера, пользователь числовым выбором определяет какой город 
        /// необходимо сохранить в файл.
        /// </summary>
        /// <param name="formalListCities"></param>
        public void ShowReceivedCities(List<RootBasicCityInfo> formalListCities)
        {
            textWorker.ShowSavedCity(formalListCities);

            Console.Write(textMessages.SaveCityToMonitor);
            bool correctInput = false;
            do
            {
                int cityNum;
                while (!int.TryParse(Console.ReadLine(), out cityNum))
                {
                    textWorker.ShowTheText(textMessages.IntParseError);
                }
                try
                {
                    ListOfCitiesForMonitoringWeather.Add(formalListCities[cityNum - 1]);
                    correctInput= true;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    textWorker.ShowTheText(textMessages.IncorrectInput);
                    textWorker.ShowTheText(ex.Message);
                }
            }
            while(!correctInput);
            WriteListOfCityMonitoring();
        }
    }
}
