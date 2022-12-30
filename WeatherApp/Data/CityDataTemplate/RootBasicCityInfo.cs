using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZone = WeatherApp.TimeZone;

namespace WeatherApp
{
    /// <summary>
    /// Базовая структура данных по городам которую возвращает сервер
    /// https://www.accuweather.com/
    /// </summary>
    /// <param name="Version"></param>
    /// <param name="Key"></param>
    /// <param name="Type"></param>
    /// <param name="Rank"></param>
    /// <param name="LocalizedName"></param>
    /// <param name="EnglishName"></param>
    /// <param name="PrimaryPostalCode"></param>
    /// <param name="IsAlias"></param>
    /// <param name="Region"></param>
    /// <param name="Country"></param>
    /// <param name="AdministrativeArea"></param>
    /// <param name="TimeZone"></param>
    /// <param name="GeoPosition"></param>
    /// <param name="SupplementalAdminAreas"></param>
    /// <param name="DataSets"></param>
    public record RootBasicCityInfo(
        int Version,
        string Key,
        string Type,
        int Rank,
        string LocalizedName,
        string EnglishName,
        string PrimaryPostalCode,
        bool IsAlias,
        Region Region,
        Country Country,
        AdministrativeArea AdministrativeArea,
        TimeZone TimeZone,
        GeoPosition GeoPosition,
        List<SupplementalAdminAreas> SupplementalAdminAreas,
        List<string> DataSets
        );

    
  
   




}
