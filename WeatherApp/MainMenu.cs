using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace WeatherApp
{
    public class MainMenu
    {
        //private UserApiManager userApiManager;
        //private SearcherCity searcherCity;
        private ReceiverWeather receiverWeather;
        private TextMessages textMessages;

        public MainMenu()
        {
            textMessages = new TextMessages();  
            receiverWeather = new ReceiverWeather();
            receiverWeather.SearcherCity.ApiManager.ReadUserApiFromLocalStorage();
            receiverWeather.SearcherCity.DataRepo.ReadListOfCityMonitoring();
            GetTheMainMenu();
            
        }
        private void GetTheMainMenu()
        {
            bool canExit = true;
            string? answer;
            while(canExit)
            {
                WriteLine(textMessages.OpeningMenu);

                Write(textMessages.GetChoice);
                answer = ReadLine()?.ToLowerInvariant().Trim();
                switch(answer)
                {
                    case "1":
                        var api = ReadLine().Trim();
                        receiverWeather.SearcherCity.ApiManager.WriteUserApiToLocalStorage(api);
                        break;
                    case "2":
                        Write(textMessages.ChooseLang);
                        var searchLanguage = ReadLine().Trim().ToLowerInvariant();
                        Write(textMessages.GetCityName);
                        var nameOfCity = ReadLine().Trim();
                        receiverWeather.SearcherCity.GettingListOfCitesOnRequest(nameOfCity, searchLanguage);
                        break;
                    case "3":
                        receiverWeather.GetWeatherDataFromServer();
                        break;
                    case "4":
                        receiverWeather.SearcherCity.RemoveCityFromList();
                        break;
                    case "q":
                    case "й":
                        canExit = false;
                        break;
                    default:
                        WriteLine(textMessages.IncorrectInput);
                        break;
                }

            }
        }
    }
}
