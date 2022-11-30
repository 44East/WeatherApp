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
    public record TimeZone(string Code, string Name, double GmtOffset, bool IsDaylightSaing, object NextOffsetChange);
    public record SupplementalAdminAreas(int Level, string LocalizedName, string EnglishName);
    public record Region(string ID, string LocalizedName, string EnglishName);
    public record Country(string ID, string LoacalizedName, string EnglishName);
    public record AdministrativeArea(string ID, string LocalizedName, string EnglishName, int Level, string LocalizedType, string EnglishType, string CountryID);
    public record Elevation(Metric Metric, Imperial Imperial);
    public record GeoPosition(double Latitude, double Longitude, Elevation Elevation);
    public record Imperial(double Value, string Unit, int UnitType);
    public record Metric(double Value, string Unit, int UnitType);



}
