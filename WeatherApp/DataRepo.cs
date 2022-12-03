using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeatherApp
{
    public class DataRepo
    {
        private TextMessages textMessages;
        public DataRepo()
        {
            textMessages= new TextMessages();
            ListOfCitiesForMonitoringWeather = new List<RootBasicCityInfo>();
        }
        /// <summary>
        /// Временно хранит прочитанные города из файла с локального диска, для дальнейшего вывода по ним погоды. 
        /// </summary>
        public List<RootBasicCityInfo> ListOfCitiesForMonitoringWeather { get; private set; }   
        /// <summary>
        /// При запуске читает локальнй файл сохраненных городов и записывает их в коллекцию, если файл еще не создан или
        /// удален/перемещен, то метод создает пустой файл
        /// </summary>
        public void ReadListOfCityMonitoring()
        {        
            
            try
            {
                using StreamReader sr = new StreamReader("RootBasicCityInfo.json");
                var prepareString = sr.ReadToEnd();
                ListOfCitiesForMonitoringWeather = JsonSerializer.Deserialize<List<RootBasicCityInfo>>(prepareString);
                sr.Dispose(); 
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine(textMessages.CityFileDoesntExist);
                CreateFileRBCI();
                Console.WriteLine(ex.Message);
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }           
            
        }
        /// <summary>
        /// Создает пустой файл для хранения базовой информации о найденных городах
        /// </summary>
        private void CreateFileRBCI()
        {
            using var file = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RootBasicCityInfo.json"));
            file.Dispose();
        }
        /// <summary>
        /// Записывает в файл все изменения такие как добавление нового города или удаление города из списка.
        /// </summary>
        private void WriteListOfCityMonitoring()
        {

            using StreamWriter sw = new StreamWriter("RootBasicCityInfo.json");
            Stream stream = sw.BaseStream;
            JsonSerializer.Serialize<List<RootBasicCityInfo>>(stream,ListOfCitiesForMonitoringWeather);
            
            sw.Dispose();
        }
        /// <summary>
        /// Удаляет выбранный пользователем экземпляр города из коллекции и записывает изменения в файл
        /// </summary>
        /// <param name="rootBasicCityInfo"></param>
        public void RemoveCityFromSavedList(RootBasicCityInfo rootBasicCityInfo)
        {
            if(rootBasicCityInfo == null)
            {
                Console.WriteLine(textMessages.ListIsEmpty); 
                return;
            }
            ListOfCitiesForMonitoringWeather.Remove(rootBasicCityInfo);
            WriteListOfCityMonitoring();
        }
        /// <summary>
        /// Принимает временную коллекцию городов которую вернул поиск с сервера, пользователь числовым выбором определяет какой город 
        /// необходимо сохранить в файл
        /// </summary>
        /// <param name="formalListCities"></param>
        public void PrintReceivedCities(List<RootBasicCityInfo> formalListCities)
        {
            foreach (var item in formalListCities)
            {
                Console.WriteLine(textMessages.PatternOfCity, formalListCities.IndexOf(item) + 1, item.EnglishName,
                                  item.LocalizedName, item.Country.LoacalizedName,
                                  item.AdministrativeArea.LocalizedName, item.AdministrativeArea.LocalizedType);
                
            }
            Console.Write(textMessages.SaveCityToMonitor);
            int cityNum;
            while (!int.TryParse(Console.ReadLine(), out cityNum))
            {
                Console.WriteLine(textMessages.IntParseError);
            }
            try
            {
                ListOfCitiesForMonitoringWeather.Add(formalListCities[cityNum - 1]);
            }
            catch(Exception ex)
            {
                Console.WriteLine(textMessages.IncorrectInput);
                Console.WriteLine(ex.Message);
            }
            WriteListOfCityMonitoring();
        }
    }
}
