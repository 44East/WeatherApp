using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZone = WeatherApp.TimeZone;

namespace WeatherApp
{
    //public class RootBasicCityInfo
    //{
    //    public int Version { get; set; }
    //    public string Key { get; set; }
    //    public string Type { get; set; }
    //    public int Rank { get; set; }
    //    public string LocalizedName { get; set; }
    //    public string EnglishName { get; set; }
    //    public string PrimaryPostalCode { get; set; }
    //    public Region Region { get; set; }
    //    public Country Country { get; set; }
    //    public AdministrativeArea AdministrativeArea { get; set; }
    //    public TimeZone TimeZone { get; set; }
    //    public GeoPosition GeoPosition { get; set; }
    //    public bool IsAlias { get; set; }
    //    public List<SupplementalAdminAreas> SupplementalAdminAreas { get; set; }
    //    public List<string> DataSets { get; set; }
    //}

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
