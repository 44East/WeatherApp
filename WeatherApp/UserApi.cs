using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    /// <summary>
    /// Строка для хранения прочитанного API ключа из файла
    /// </summary>
    public class UserApi
    {
        public UserApi() { }
        public UserApi(string userApi) => UserApiProperty = userApi;
        public string UserApiProperty { get; set; }
    }
}
