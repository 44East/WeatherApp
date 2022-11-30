using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    /// <summary>
    /// Набор текстовых сообщений, при расширении приложении или добавления других языков, весь текстовый массив собран в одном месте
    /// </summary>
    /// <param name="PatternOfCity"></param>
    /// <param name="PatternOfWeather"></param>
    /// <param name="ErorrsBySearch"></param>
    /// <param name="ApiFileDoesntExist"></param>
    /// <param name="CityFileDoesntExist"></param>
    /// <param name="GetCityNum"></param>
    /// <param name="IntParseError"></param>
    /// <param name="CityNoExist"></param>
    /// <param name="OpeningMenu"></param>
    /// <param name="GetChoice"></param>
    /// <param name="ChooseLang"></param>
    /// <param name="GetCityName"></param>
    /// <param name="IncorrectInput"></param>
    /// <param name="SaveCityToMonitor"></param>
    /// <param name="ListIsEmpty"></param>
    /// <param name="ApiIsEmpty"></param>
    public record TextMessages
    (
        string PatternOfCity = "=====\n" + "Номер в списке: {0}\n" + "Название в оригинале: {1}\n"
            + "В переводе:  {2} \n" + "Страна: {3}\n" + "Административный округ: {4}\n" + "Тип: {5}\n" + "====\n",
        string PatternOfWeather = "=====\n" + "Город: {5}\n" + "Дата: {0}\n" + "Температура минимальная: {1}\n"
            + "Температура максимальная: {2}\n" + "Примечание на день: {3}\n" + "Примечание на ночь: {4}\n" + "====\n",
        string ErorrsBySearch = "Не получилось отобразить запрашиваемый город.\n" + "Возможные причины: \n" +
                "* Неправильно указано название города\n" + "* Нет доступа к интернету\n" + "Подробнее ниже: \n",
        string ApiFileDoesntExist = "Вы еще не добавили ни одного ключа доступа, воспользуйтесь соответствующим пунктом меню!",
        string CityFileDoesntExist = "Похоже файл с сохраненными городами удален, пермещен или еще не был создан.",
        string GetCityNum = "Номер города для действия: ",
        string IntParseError = "Ввод должен содержать только числовой набор символов!\n",
        string CityNoExist = "Такого номера нет. Попробуйте еще раз.",
        string OpeningMenu = "=================================================\n" +
                             "|Добро пожаловать в Погоду                      |\n" +
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
        string IncorrectInput = "Некорректный ввод!\n",
        string SaveCityToMonitor = "Номер какого города добавить в мониторинг: ",
        string ListIsEmpty = "Список городов пуст, добавьте город в список!",
        string ApiIsEmpty = "Ваш API ключ недоступен, добавьте его вновь, возможно файл был удален или перемещен\nБез ключа вы не сможете осуществлять поиск!"
        );

}
