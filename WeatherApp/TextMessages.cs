

namespace WeatherApp
{
    /// <summary>
    /// Набор текстовых сообщений, при расширении приложения или добавления других языков, весь текстовый массив собран в одном месте.
    /// </summary>
    public record TextMessages
    (
        string PatternOfCity = "=====\n" +
                               "Номер в списке: {0}\n" +
                               "Название в оригинале: {1}\n" +
                               "В переводе:  {2} \n" +
                               "Страна: {3}\n" +
                               "Административный округ: {4}\n" +
                               "Тип: {5}\n" +
                               "====\n",
        string PatternOfWeather = "=====\n" +
                                  "Город: {5}\n" +
                                  "Дата: {0}\n" +
                                  "Температура минимальная: {1}\n" +
                                  "Температура максимальная: {2}\n" +
                                  "Примечание на день: {3}\n" +
                                  "Примечание на ночь: {4}\n" +
                                  "====\n",

        string ErorrsBySearch = "Не получилось отобразить запрашиваемый город.\n" +
                                "Возможные причины: \n" +
                                "* Неправильно указан язык поиска\n" +
                                "* Не правильно указано название города\n" +
                                "Подробнее ниже: \n",

        string ApiFileDoesntExist = "Вы еще не добавили ни одного ключа доступа, либо текущий ключ недействителен." +
                                    "\nВоспользуйтесь соответствующим пунктом меню и добавьте новый ключ!",
        string CityFileDoesntExist = "Похоже файл с сохраненными городами удален, пермещен или еще не был создан.",
        string CityFileFailure = "Файл с городами пуст или нечитаем, добавьте новые города в список",
        string CityListWritingFailure = "Ошибка записи в файл, список городов поврежден",
        string GetCityNum = "Номер города для действия: ",
        string IntParseError = "Ввод должен содержать только числовой набор символов!\n",
        string CityNoExist = "Такого номера нет. Попробуйте еще раз.",
        string OpeningMenu = "=================================================\n" +
                             "|Добро пожаловать в Погоду                      |",
        string MainMenuMsg = "=================================================\n" +
                             "|Доступные действия:                            |\n" +
                             "=================================================\n" +
                             "|1 - Ввести API                                 |\n" +
                             "|2 - Добавить город                             |\n" +
                             "|3 - Посмотреть погоду из сохраненныех городов  |\n" +
                             "|4 - Удалить город из сохраненного списка       |\n" +
                             "|q - Выйти из программы                         |\n" +
                             "=================================================\n",
        string GetChoice = "Ваш выбор: ",
        string ChooseLang = "Язык поиска(ru, en): ",
        string GetCityName = "\nВведите название города: ",
        string Waiting = "\nДля продолжения нажмите любую клавишу.",
        string IncorrectInput = "Некорректный ввод!\n",
        string SaveCityToMonitor = "Номер какого города добавить в мониторинг: ",
        string CityListIsEmpty = "Список городов пуст, добавьте город в список!",
        string ApiIsEmpty = "Ваш API ключ недоступен, добавьте его вновь, возможно файл был удален или перемещен\nБез ключа вы не сможете осуществлять поиск!",
        string NetworkOrHostIsNotAwailable = "Похоже отсутствует соединение с интернетом или запрашиваемый сервер недоступен.",
        string SearchError = "Список найденных городов пуст, т.к. поиск завершился неудачей.",
        string ReceiveWeatherError = "Выгрузка погоды с сервера завершилась неудачей, повторите попытку.",
        string CityFileName = "RootBasicCityInfo.json",
        string ApiKeysFileName = "UserApiKeys.json"
        )
    {
        ///<summary>
        ///Ссылка для поиска города на сервере.
        ///{0} = apKey, {1} = cityName, {2} = searchLanguage
        ///</summary>
        public string SearchCityUrl { get; } = "http://dataservice.accuweather.com/locations/v1/cities/search?apikey={0}&q={1}&language={2}"; //{0} = apKey, {1} = cityName, {2} = searchLanguage
        ///<summary>
        ///Ссылка для вывода погоды по конкретному городу на 5 дней.
        ///{0} = CurrentCity.Key, {1} = apiKey
        ///</summary>
        public string GetWeatherUrl { get; } = "http://dataservice.accuweather.com/forecasts/v1/daily/5day/{0}?apikey={1}&language=ru&metric=true"; //{0} = CurrentCity.Key, {1} = apiKey

    }

}
