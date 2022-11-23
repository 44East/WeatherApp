using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.CityAllInformation
{
    public record AdministartiveArea(string ID, string LocalizedName,string EnglishName, int Level,string LocalizedType, string EnglishType, string CountryID);
}
