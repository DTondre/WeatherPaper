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
    }
}
