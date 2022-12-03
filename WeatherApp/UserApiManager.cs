using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeatherApp
{
    public class UserApiManager
    {
        private TextMessages textMessages;
        public UserApiManager()
        {
            textMessages = new TextMessages();
            userApiList = new ObservableCollection<UserApi>();
        }

        /// <summary>
        /// Коллекция для хранения API ключей, коллекция подходит при использовании коммерческого доступа и хранении нескольких ключей, 
        /// соотсветсвенно можно реализовать соотвествующий выбор при запуске 
        /// </summary>
        public ObservableCollection<UserApi> userApiList { get; private set; }
        /// <summary>
        /// Метод записывает API ключи в файл
        /// </summary>
        /// <param name="formalUserAPi"></param>
        public void WriteUserApiToLocalStorage(string formalUserAPi)
        {
            UserApi userApiProp = new UserApi { UserApiProperty = formalUserAPi };
            userApiList.Add(userApiProp);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<UserApi>));

            using (StreamWriter sw = new StreamWriter("UserApi.xml", true))
            {
                xmlSerializer.Serialize(sw, userApiList);
            }
        }
        /// <summary>
        /// При запуске всегда проверяется наличие файла ключей и читается информация из него,
        /// если файл не создан или отсутствует, выводится соответствующее сообщение и создается файл.
        /// </summary>
        public void ReadUserApiFromLocalStorage()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<UserApi>));
            try
            {
                using (StreamReader sr = new StreamReader("UserApi.xml"))
                {
                    userApiList = xmlSerializer.Deserialize(sr) as ObservableCollection<UserApi>;
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(textMessages.ApiFileDoesntExist);
                Console.WriteLine(ex.Message);
                CreateFileUserApi();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);                
            }
        }
        /// <summary>
        /// Создает пустой файл для API ключей
        /// </summary>
        private void CreateFileUserApi()
        {
            using var file = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UserApi.xml"));
        }
    }

}

