using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPaper
{
    public static class Constants
    {
        public const string weatherCurrentWeather = "&current_weather=true";
        public const string weatherBaseUri = "https://api.open-meteo.com/v1/";
        public const string weatherForecastRequest = "forecast";

        //public const string wallpaperBaseUri = "https://wallhaven.cc/api/v1/search";
        public const string wallpaperBaseUri = "https://api.unsplash.com/photos/random?client_id=2RfrPF5cZPBQYi4_rNxO2X4bcfyXmYVb56s0jtFhML0&count=10&orientation=landscape";
       
    }
}
