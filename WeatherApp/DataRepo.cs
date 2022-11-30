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
                Console.WriteLine(textMessages.CityFileDoesntExist);
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
        public void RemoveCityFromSavedList(RootBasicCityInfo rootBasicCityInfo)
        {
            ListOfCitiesForMonitoringWeather.Remove(rootBasicCityInfo);
            WriteListOfCityMonitoring();
        }

        public void PrintReceivedCities(ObservableCollection<RootBasicCityInfo> formalListCities)
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
