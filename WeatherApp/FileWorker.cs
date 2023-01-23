using System.Text.Json;

namespace WeatherApp
{
    /// <summary>
    /// Файловый обработчик, читает и записывает файлы.
    /// Дженерик класс, в виду того что данные коллекций имеют разное наполнение, каждый новый экземпляр имеет неоходимый тип для вызывающего класса.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FileWorker<T> where T : class
    {
        /// <summary>
        /// Читает файл с жесткого диска, принимет название файла которое необходимо прочитать.
        /// Если файл отсутствует, создает пустой файл и пробрасывает исключение для вызывающего кода.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public List<T> ReadFileFromLocalDisk(string fileName)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(fileName);
                using StreamReader sr = fileInfo.OpenText();
                var prepareString = sr.ReadToEnd();

                return JsonSerializer.Deserialize<List<T>>(prepareString);
            }
            catch(FileNotFoundException ex)
            {
                Task.Run(async ()=> await CreateFileAsync(fileName));
                throw ex;
            }
        }
        /// <summary>
        /// Создает пустой файл с необходимым именем, не блокируя вызывающий поток.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private async Task CreateFileAsync(string fileName)
        {
            int bufferSize = 2 << 11;//4096 bytes, it's default buffer size in FileStream.
            await using var fs = new FileStream(fileName, 
                                                FileMode.Create, 
                                                FileAccess.Write, 
                                                FileShare.None, 
                                                bufferSize, 
                                                FileOptions.Asynchronous);                                                
        }        
        /// <summary>
        /// Записывает полученную коллекцию от вызывающего кода в необходимое название файла.
        /// </summary>
        /// <param name="currList"></param>
        /// <param name="fileName"></param>
        public void WriteFileToLocalStorage(List<T> currList, string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            using FileStream fs = fileInfo.OpenWrite();
            JsonSerializer.Serialize(fs, currList);
        }        

    }
}
