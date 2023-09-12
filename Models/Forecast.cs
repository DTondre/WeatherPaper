using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPaper.Models
{
    public class Forecast
    {
        public string timezone { get; set; }
        public CurrentWeather current_weather { get; set; }

    }
}
