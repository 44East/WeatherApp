using static System.Console;

namespace WeatherApp
{
    /// <summary>
    /// Обслуживающий класс для работы со строковыми сообщениями и выводом их на консоль. 
    /// </summary>
    public class TextWorker
    {
        public delegate void ShowTextHandler(string text);
        public event ShowTextHandler ShowText = null;


        private TextMessages textMessages;
        public TextWorker(TextMessages textMessages)
        {
             this.textMessages = textMessages;
        }

        /// <summary>
        /// Выводит текстовые сообщения, метод для присвоения делегату, сигнатура может быть переназначена, например для вывода в TextBox(WPF).
        /// </summary>
        /// <param name="text"></param>
        public static void OutputText(string text) => WriteLine(text);
        /// <summary>
        /// Инциализация события для вывода текстового сообщения.
        /// </summary>
        /// <param name="text"></param>
        public void ShowTheText(string text)
        {
            ShowText?.Invoke(text);
        }
        /// <summary>
        /// Выводит на коносль погоду полученную с сервера по выбранному экземпляру города.
        /// </summary>
        /// <param name="currentCity"></param>
        /// <param name="rootWeather"></param>
        public void ShowWeatherInCurrentCity(RootBasicCityInfo currentCity, RootWeather rootWeather)
        {
                
                foreach (var item in rootWeather.DailyForecasts)
                {
                    ForegroundColor = item.Temperature.Minimum.Value < 0.0d && item.Temperature.Maximum.Value < 0.0d ? ConsoleColor.DarkBlue : item.Temperature.Minimum.Value < 0.0d ? ConsoleColor.Cyan : ConsoleColor.Yellow;
                    WriteLine(textMessages.PatternOfWeather, item.Date, item.Temperature.Minimum.Value,
                    item.Temperature.Maximum.Value, item.Day.IconPhrase, item.Night.IconPhrase, currentCity.LocalizedName);
                    ResetColor();

                }
        }
        /// <summary>
        /// Принимает коллекцию городов из файла, либо коллекцию городов из поиска с сервера для вывода в консоль.
        /// </summary>
        /// <param name="cityList"></param>
        /// <exception cref="NullReferenceException"></exception>
        public void ShowSavedCity(List<RootBasicCityInfo> cityList) 
        {
            foreach (var item in cityList ?? throw new NullReferenceException())
            {
                WriteLine(textMessages.PatternOfCity, cityList.IndexOf(item) + 1,
                item.EnglishName, item.LocalizedName, item.Country.LoacalizedName,
                item.AdministrativeArea.LocalizedName, item.AdministrativeArea.LocalizedType); 
            }
        }
    }
}
