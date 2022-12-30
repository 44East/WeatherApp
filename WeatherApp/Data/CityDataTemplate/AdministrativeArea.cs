using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public record AdministrativeArea
    (
        string ID,
        string LocalizedName,
        string EnglishName,
        int Level,
        string LocalizedType,
        string EnglishType,
        string CountryID
        );
}
