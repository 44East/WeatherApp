using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.CityAllInformation;
using TimeZone = WeatherApp.CityAllInformation.TimeZone;

namespace WeatherApp
{
    public class RootBasicCityInfo
    {
        public int Version { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public int Rank { get; set; }
        public string LocalizedName { get; set; }
        public string EnglishName { get; set; }
        public string PrimaryPostalCode { get; set; }
        public bool IsAlias { get; set; }
        public Region Region { get; set; }
        public Country Country { get; set; }
        public AdministartiveArea AdministartiveArea { get; set; }
        public TimeZone TimeZone { get; set; }
        public GeoPosition GeoPosition { get; set; }
        public List<SupplementalAdminAreas>  SupplementalAdminAreas { get; set; }
        public List<string> DataSets { get; set; }

    }
    

    
}
