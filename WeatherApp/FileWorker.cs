using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApp
{
    /// <summary>
    /// Файловый обработчик, читает и записывает файлы.
    /// Дженерик класс, в виду того что данные коллекций имеют раное наполнение, каждый новый экземпляр имеет неоходимы тип для вызывающего класса
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FileWorker<T> where T : class
    {
        /// <summary>
        /// Читает файл с жесткого диска, принимет название файла которое необходимо прочитать,
        /// Если файл отсутствует, создает пустой файл и пробрасывает исключение для вызывающего кода.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public List<T> ReadFileFromLocalDisk(string fileName)
        {
            JsonSerializerOptions jsOptions = new JsonSerializerOptions();
            jsOptions.IgnoreReadOnlyProperties = true;
            try
            {
                using StreamReader sr = new StreamReader(fileName);
                var prepareString = sr.ReadToEnd();

                return JsonSerializer.Deserialize<List<T>>(prepareString, jsOptions);
            }
            catch(FileNotFoundException ex)
            {
                Task.Run(async ()=> await CreateFileAsync(fileName));
                throw ex;
            }
        }
        /// <summary>
        /// Создает пустой файл с необходимым именем в фоновом режиме
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private async Task CreateFileAsync(string fileName)
        {
            await using var file = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName));
        }        
        /// <summary>
        /// Записывает полученную коллекцию от вызывающего кода в необходимое название файла
        /// </summary>
        /// <param name="currList"></param>
        /// <param name="fileName"></param>
        public void WriteFileToLocalStorage(List<T> currList, string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                Stream stream = sw.BaseStream;
                JsonSerializer.Serialize(stream, currList);
            }
        }        

    }
}
