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
        public Action<string> Action = new Action<string>(TextWorker.OutputText);
        private HttpWorker httpWorker;
        private ReceiverWeather receiverWeather;
        private TextMessages textMessages;
        /// <summary>
        /// Создает все необходимые для работы экземпляры классов, запуск приложения
        /// </summary>
        public MainMenu()
        {
            SetWindowSize(50, 45);
            httpWorker= new HttpWorker();
            textMessages = new TextMessages();  
            receiverWeather = new ReceiverWeather();
            receiverWeather.SearcherCity.ApiManager.ReadUserApiFromLocalStorage();
            receiverWeather.SearcherCity.DataRepo.ReadListOfCityMonitoring();
            GetTheMainMenu();
            
        }
        /// <summary>
        /// Вывод главного меню поддерживает числовой выбор пунктов(в строковом формате)
        /// </summary>
        private void GetTheMainMenu()
        {
            WriteLine(textMessages.OpeningMenu);
            bool canExit = true;
            string? answer;
            while(canExit)
            {
                WriteLine(textMessages.MainMenuMsg);

                Write(textMessages.GetChoice);
                answer = ReadLine()?.ToLowerInvariant().Trim();
                switch(answer)
                {
                    case "1":
                        var api = ReadLine().Trim();
                        receiverWeather.SearcherCity.ApiManager.WriteUserApiToLocalStorage(api);
                        GetWaitAndClear();
                        break;
                    case "2":
                        Write(textMessages.ChooseLang);
                        var searchLanguage = ReadLine().Trim().ToLowerInvariant();
                        Write(textMessages.GetCityName);
                        var nameOfCity = ReadLine().Trim();
                        receiverWeather.SearcherCity.GettingListOfCitesOnRequest(httpWorker, nameOfCity, searchLanguage);
                        GetWaitAndClear();
                        break;
                    case "3":
                        receiverWeather.GetWeatherDataFromServer(httpWorker);
                        GetWaitAndClear();
                        break;
                    case "4":
                        receiverWeather.SearcherCity.RemoveCityFromList();
                        GetWaitAndClear();
                        break;
                    case "q":
                    case "й":
                        canExit = false;
                        break;
                    default:
                        WriteLine(textMessages.IncorrectInput);
                        GetWaitAndClear();
                        break;
                }

            }
        }
        private void GetWaitAndClear()
        {
            Write(textMessages.Waiting);
            ReadKey();
            Clear();
        }
        
    }
}
