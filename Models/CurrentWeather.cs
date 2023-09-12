using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPaper.Models
{
    public class CurrentWeather
    {
        public float temperature { get; set; }
        public float windspeed { get; set; }
        public int winddirection { get; set; }
        public bool isday {  get; set; }
    }
}
