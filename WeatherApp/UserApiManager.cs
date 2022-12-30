using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace WeatherApp
{
    public class UserApiManager
    {
        private TextMessages textMessages;
        private TextWorker textWorker;
        public UserApiManager(TextMessages textMessages, TextWorker textWorker)
        {            
            this.textMessages = textMessages;
            this.textWorker = textWorker;
            UserApiList = new List<UserApi>();
        }

        /// <summary>
        /// Коллекция для хранения API ключей, коллекция подходит при использовании коммерческого доступа и хранении нескольких ключей, 
        /// соотсветсвенно можно реализовать соотвествующий выбор при запуске 
        /// </summary>
        public List<UserApi> UserApiList { get; private set; }
        /// <summary>
        /// Метод записывает API ключи в файл
        /// </summary>
        /// <param name="formalUserAPi"></param>
        public void WriteUserApiToLocalStorage(string formalUserAPi)
        {   
            UserApi userApiProp = new UserApi { UserApiProperty = formalUserAPi };
            UserApiList.Add(userApiProp);

            using (StreamWriter sw = new StreamWriter("UserApiKeys.json", false))
            {
                Stream stream = sw.BaseStream;
                JsonSerializer.Serialize(stream, UserApiList);
            }
        }
        /// <summary>
        /// При запуске всегда проверяется наличие файла ключей и читается информация из него,
        /// если файл не создан или отсутствует, выводится соответствующее сообщение и создается файл.
        /// </summary>
        public void ReadUserApiFromLocalStorage()
        {
            try
            {
                using(StreamReader sr = new StreamReader("UserApiKeys.json"))
                {
                    Stream stream = sr.BaseStream;
                    UserApiList = JsonSerializer.Deserialize<List<UserApi>>(stream);
                }
            }
            catch(JsonException ex)
            {
                textWorker.ShowTheText(textMessages.ApiFileDoesntExist);
                textWorker.ShowTheText(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                textWorker.ShowTheText(textMessages.ApiFileDoesntExist);
                textWorker.ShowTheText(ex.Message);
                CreateFileUserApiAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Создает пустой файл для API ключей
        /// </summary>
        private async Task CreateFileUserApiAsync()
        {
           await using var file = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UserApiKeys.json"));
        }
    }

}

