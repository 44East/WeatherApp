using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public static class TextWorker
    {
        /// <summary>
        /// Выводит текстовые сообщения, метод для присвоения делегату, сигнатура может быть переназначена, например для вывода в TextBox(WPF)
        /// </summary>
        /// <param name="text"></param>
        public static void OutputText(string text) => Console.WriteLine(text);
    }
}
