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
        public DataRepo()
        {
            ListOfCitiesForMonitoringWeather = new ObservableCollection<RootBasicCityInfo>();
        }




        /// <summary>
        /// Временно хранит прочитанные города из файла с локального диска, для дальнейшего вывода по ним погоды. 
        /// </summary>
        public ObservableCollection<RootBasicCityInfo> ListOfCitiesForMonitoringWeather { get; private set; }   
        
        public void ReadListOfCityMonitoring()
        {        
            using StreamReader sr = new StreamReader("RootBasicCityInfo.json");
            try
            {
                var prepareString = sr.ReadToEnd();
                ListOfCitiesForMonitoringWeather = JsonSerializer.Deserialize<ObservableCollection<RootBasicCityInfo>>(prepareString);
                
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine("Похоже файл с сохраненными городами удален, пермещен или еще не был создан.");
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            sr.Dispose();
        }

        private void WriteListOfCityMonitoring()
        {

            using StreamWriter sw = new StreamWriter("RootBasicCityInfo.json");
            Stream stream = sw.BaseStream;
            JsonSerializer.Serialize<ObservableCollection<RootBasicCityInfo>>(stream,ListOfCitiesForMonitoringWeather);
            
            sw.Dispose();
        }
        public void RemoveCityFromSavedList()
        {

        }

        public void PrintReceivedCities(ObservableCollection<RootBasicCityInfo> formalListCities)
        {
            string pattern = "=====\n" + "Номер в списке: {0}\n" + "Название в оригинале: {1}\n"
            + "В переводе:  {2} \n" + "Страна: {3}\n" + "Административный округ: {4}\n"
            + "Тип: {5}\n" + "====\n";
            int numberInList = 0;

            foreach (var item in formalListCities)
            {
                Console.WriteLine(pattern, numberInList.ToString(), item.EnglishName,
                                  item.LocalizedName, item.Country.LoacalizedName,
                                  item.AdministrativeArea.LocalizedName, item.AdministrativeArea.LocalizedType);
                numberInList++;
            }
            Console.Write("Номер какого города добавить в мониторинг: ");
            int cityNum;
            while (!int.TryParse(Console.ReadLine(), out cityNum))
            {
                Console.WriteLine("Ввод может содержать только числовой набор символов!");
            }
            try
            {
                ListOfCitiesForMonitoringWeather.Add(formalListCities[cityNum]);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Похоже вы ошиблись цифрой.\n");
                Console.WriteLine(ex.Message);
            }
            WriteListOfCityMonitoring();
        }
    }
}
