using WeatherPaper.Models;

namespace WeatherPaper.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<Forecast> GetWeatherInfoAsync();
    }
}
