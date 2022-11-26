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

        public MainMenu()
        {
           
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
                WriteLine("==================================");
                WriteLine("|Добро пожаловать в Погоду       |");
                WriteLine("|Доступные действия:             |\n" +
                          "==================================");
                WriteLine("|1 - Ввести API                  |\n" +
                          "|2 - Добавить город              |\n" +
                          "|3 - Посмотреть погоду           |\n" +
                          "|q - Выйти из программы          |");
                WriteLine("===============================\n");

                Write("Ваш выбор: ");
                answer = ReadLine()?.ToLowerInvariant().Trim();
                switch(answer)
                {
                    case "1":
                        var api = ReadLine().Trim();
                        receiverWeather.SearcherCity.ApiManager.WriteUserApiToLocalStorage(api);
                        break;
                    case "2":
                        Write("Язык поиска(ru, en): ");
                        var searchLanguage = ReadLine().Trim().ToLowerInvariant();
                        Write("\nВведите название города: ");
                        var nameOfCity = ReadLine().Trim();
                        receiverWeather.SearcherCity.GettingListOfCitesOnRequest(nameOfCity, searchLanguage);
                        break;
                    case "3":
                        receiverWeather.GetWeatherDataFromServer();
                        break;
                    case "q":
                    case "й":
                        canExit = false;
                        break;
                    default:
                        WriteLine("Некорректный ввод!");
                        break;
                }

            }
        }
    }
}
