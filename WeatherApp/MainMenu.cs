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
        private HttpWorker httpWorker;
        private ReceiverWeather receiverWeather;
        private TextMessages textMessages;
        private TextWorker textWorker;
        private DataRepo dataRepo;
        private SearcherCity searcherCity;
        private UserApiManager apiManager;



        /// <summary>
        /// Создает все необходимые для работы экземпляры классов, запуск приложения, 
        /// Проброс неоходимых экземпляров для всех рабочих классов
        /// Инициализация события для вывода текстовых сообщений
        /// </summary>
        public MainMenu()
        {
            textMessages = new TextMessages();
            textWorker = new TextWorker(textMessages);
            textWorker.ShowText += TextWorker.OutputText;

            dataRepo = new DataRepo(textMessages, textWorker);
            apiManager = new UserApiManager(textMessages, textWorker);

            searcherCity = new SearcherCity(textMessages, textWorker, apiManager, dataRepo);

            receiverWeather = new ReceiverWeather(textMessages, textWorker, searcherCity, apiManager);

            httpWorker = new HttpWorker();

            InitializeUserFiles();
            GetTheMainMenu();

        }

        /// <summary>
        /// Вывод главного меню поддерживает числовой выбор пунктов(в строковом формате)
        /// 1. Ввод нового API для доступа к серверу поиск/выдача погоды
        /// 2. Добавление нового города, в коллекцию, с возможностью выбора языка поиска.
        /// 3. Просмотр погоды по сохраненным городам.
        /// 4. Удаление города из коллекции
        /// 5. Выход
        /// </summary>
        private void GetTheMainMenu()
        {
            textWorker.ShowTheText(textMessages.OpeningMenu);
            bool canExit = true;
            string? answer;
            while (canExit)
            {
                textWorker.ShowTheText(textMessages.MainMenuMsg);

                Write(textMessages.GetChoice);
                answer = ReadLine()?.ToLowerInvariant().Trim();
                switch (answer)
                {
                    case "1":
                        var api = ReadLine()?.Trim();
                        apiManager.WriteUserApiToLocalStorage(api);
                        GetWaitAndClear();
                        break;
                    case "2":
                        Write(textMessages.ChooseLang);
                        var searchLanguage = ReadLine()?.Trim().ToLowerInvariant();
                        Write(textMessages.GetCityName);
                        var nameOfCity = ReadLine().Trim();
                        searcherCity.GettingListOfCitesOnRequest(httpWorker, nameOfCity, searchLanguage);
                        GetWaitAndClear();
                        break;
                    case "3":
                        receiverWeather.GetWeatherDataFromServer(httpWorker);
                        GetWaitAndClear();
                        break;
                    case "4":
                        searcherCity.RemoveCityFromList();
                        GetWaitAndClear();
                        break;
                    case "q":
                    case "й":
                        canExit = false;
                        break;
                    default:
                        textWorker.ShowTheText(textMessages.IncorrectInput);
                        GetWaitAndClear();
                        break;
                }

            }
        }
        /// <summary>
        /// Метод для очистки консоли после каждого действия, перед каждой очисткой есть задержка до пользовательского ввода.
        /// </summary>
        private void GetWaitAndClear()
        {
            Write(textMessages.Waiting);
            ReadKey();
            Clear();
        }
        /// <summary>
        /// Читает API ключ для доступа к севреру и спиок сохраненнх городов
        /// </summary>
        private void InitializeUserFiles()
        {
            apiManager.ReadUserApiFromLocalStorage();
            dataRepo.ReadListOfCityMonitoring();
        }

    }
}
