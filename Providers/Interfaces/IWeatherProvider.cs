using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherPaper.Models;

namespace WeatherPaper.Providers.Interfaces
{
    public interface IWeatherProvider
    {
        Task<Forecast> GetWeatherInfoAsync();
    }
}
