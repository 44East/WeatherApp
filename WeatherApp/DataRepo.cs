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
        
        ObservableCollection<RootBasicCityInfo> findCitiesColection = new ObservableCollection<RootBasicCityInfo>();
        public ObservableCollection<RootBasicCityInfo> ListOfCitiesForMonitoringWeather { get; private set; }
        public ObservableCollection<RootWeather> WheatherInformation { get; private set; }

        public void ReadDataFromcalStorage()
        {
            string prepareString = null;
            ObservableCollection<RootBasicCityInfo> rbci;

            int numberInList = 0;
            string pattern = "=====\n" + "Номер в списке: {0}\n" + "Название в оригинале {1}\n" +
                             "В переводе: {2}\n" + "Страна: {3}\n" + "Административный округ: {4}\n" +
                             "Тип: {5}\n" + "====\n";
            using (StreamReader sr = new StreamReader("SimpleData.json"))
            {
                prepareString= sr.ReadToEnd();
            }
            rbci = JsonSerializer.Deserialize<ObservableCollection<RootBasicCityInfo>>(prepareString);
            foreach (var item in rbci)
            {
                Console.WriteLine(pattern, numberInList.ToString(), item.EnglishName, 
                                  item.LocalizedName,item.Country.LoacalizedName, 
                                  item.AdministrativeArea.LocalizedName, item.AdministrativeArea.LocalizedType);
                numberInList++;
            }

        }
        public void ReadWheatherData()
        {
            string prepareString = null;
            RootWeather weatherData;

            using StreamReader sr = new StreamReader("WeatherExample.json");
            prepareString = sr.ReadToEnd();

            weatherData = JsonSerializer.Deserialize<RootWeather>(prepareString);

            foreach (var item in weatherData.DailyForecasts)
            {
                Console.WriteLine(item.Temperature.Maximum.Value + " " + item.Temperature.Minimum.Value);
            }
        }
        public void ReadListOfCityMonitoring()
        {
            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<RootBasicCityInfo>));
            

            using StreamReader sr = new StreamReader("RootBasicCityInfo.json");
            try
            {
                var prepareString = sr.ReadToEnd();
                ListOfCitiesForMonitoringWeather = JsonSerializer.Deserialize<ObservableCollection<RootBasicCityInfo>>(prepareString);
                //ListOfCitiesForMonitoringWeather = xmlSerializer.Deserialize(sr) as ObservableCollection<RootBasicCityInfo>;
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void WriteListOfCityMonitoring()
        {
            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<RootBasicCityInfo>));

            using StreamWriter sw = new StreamWriter("RootBasicCityInfo.json");
            using Stream stream = sw.BaseStream;
            JsonSerializer.Serialize<ObservableCollection<RootBasicCityInfo>>(stream,ListOfCitiesForMonitoringWeather);
            //xmlSerializer.Serialize(sw, ListOfCitiesForMonitoringWeather);
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

        }
    }
}
