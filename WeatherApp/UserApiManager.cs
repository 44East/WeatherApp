using System.Text.Json;

namespace WeatherApp
{
    public class UserApiManager
    {
        private TextMessages textMessages;
        private TextWorker textWorker;
        private FileWorker<UserApi> fileWorker;
        public UserApiManager(TextMessages textMessages, TextWorker textWorker)
        {            
            this.textMessages = textMessages;
            this.textWorker = textWorker;

            fileWorker= new FileWorker<UserApi>();
            UserApiList = new List<UserApi>();
        }

        /// <summary>
        /// Коллекция для хранения API ключей, коллекция подходит при использовании коммерческого доступа и хранении нескольких ключей, 
        /// соотсветсвенно можно реализовать необходимый выбор при запуске. 
        /// </summary>
        private List<UserApi> UserApiList;
        /// <summary>
        /// Метод записывает API ключи в файл.
        /// </summary>
        /// <param name="formalUserAPi"></param>
        public void WriteUserApiToLocalStorage(string formalUserAPi)
        {
            try
            {
                if (string.IsNullOrEmpty(formalUserAPi))
                    throw new NullReferenceException(textMessages.IncorrectInput);

                UserApiList.Add(new UserApi(formalUserAPi));
                fileWorker.WriteFileToLocalStorage(UserApiList, textMessages.ApiKeysFileName);
            }
            catch(NullReferenceException ex)
            { 
                textWorker.ShowTheText(ex.Message);
                return;
            }
            catch(JsonException ex)
            {
                textWorker.ShowTheText(textMessages.ApiFileDoesntExist);
                textWorker.ShowTheText(ex.Message);
            }
        }
        /// <summary>
        /// Возвращает первый доступный ключ из коллекции если коллекция пуста, возвращает null.
        /// Полностью инкапсулирует коллекцию.
        /// </summary>
        /// <returns></returns>
        public string GetTheFirstKey()
        {
            return UserApiList?.FirstOrDefault()?.UserApiProperty ?? null;
        }
        /// <summary>
        /// При запуске всегда проверяется наличие файла ключей и читается информация из него,
        /// если файл не создан или отсутствует, выводится соответствующее сообщение и экземпляр файлового обработчика создает пустой файл 
        /// и пробрасывает соответствующее исключение.
        /// </summary>
        public void ReadUserApiFromLocalStorage()
        {
            try
            {
                UserApiList = fileWorker.ReadFileFromLocalDisk(textMessages.ApiKeysFileName); 
            }
            catch(JsonException ex)
            {
                textWorker.ShowTheText(textMessages.ApiFileDoesntExist);
                textWorker.ShowTheText(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                textWorker.ShowTheText(textMessages.ApiIsEmpty);
                textWorker.ShowTheText(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
    }

}

