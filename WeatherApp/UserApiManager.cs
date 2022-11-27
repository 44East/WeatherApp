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
        public UserApiManager()
        {
            userApiList = new ObservableCollection<UserApi>();
        }


        public ObservableCollection<UserApi> userApiList { get; private set; }
        public void WriteUserApiToLocalStorage(string formalUserAPi)
        {
            UserApi userApiProp = new UserApi { UserApiProperty = formalUserAPi };
            userApiList.Add(userApiProp);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<UserApi>));

            using (StreamWriter sw = new StreamWriter("UserApi.xml"))
            {
                xmlSerializer.Serialize(sw, userApiList);
            }
        }
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
            catch (Exception ex)
            {
                Console.WriteLine("Вы еще не добавили ни одного ключа доступа, воспользуйтесь соответствующим пунктом меню!");
                Console.WriteLine(ex.Message);
            }
        }
    }

}

