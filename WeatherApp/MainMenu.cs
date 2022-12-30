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
        private TextWorker textWorker;
        /// <summary>
        /// Создает все необходимые для работы экземпляры классов, запуск приложения
        /// </summary>
        public MainMenu()
        {
            httpWorker= new HttpWorker();
            textMessages = new TextMessages();
            textWorker = new TextWorker(textMessages);
            receiverWeather = new ReceiverWeather(textMessages, textWorker);
            textWorker.ShowText += TextWorker.OutputText;

            InitializeUserFiles();
            GetTheMainMenu();
            
        }
        
        /// <summary>
        /// Вывод главного меню поддерживает числовой выбор пунктов(в строковом формате)
        /// </summary>
        private void GetTheMainMenu()
        {
            textWorker.ShowTheText(textMessages.OpeningMenu);
            bool canExit = true;
            string? answer;
            while(canExit)
            {
                textWorker.ShowTheText(textMessages.MainMenuMsg);

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
        private void InitializeUserFiles()
        {
            receiverWeather.SearcherCity.ApiManager.ReadUserApiFromLocalStorage();
            receiverWeather.SearcherCity.DataRepo.ReadListOfCityMonitoring();
        }
        
    }
}
