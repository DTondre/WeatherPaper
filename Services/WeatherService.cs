using System.Text.Json;
using WeatherPaper.Models;
using WeatherPaper.Services.Interfaces;

namespace WeatherPaper.Services
{
    public class WeatherService : IWeatherService
    {
        private static readonly HttpClient _client = new HttpClient()
        {
            BaseAddress = new Uri(Constants.weatherBaseUri)
        };
        static readonly WindowsService _windowsService = new WindowsService();

        public async Task<Forecast> GetWeatherInfoAsync()
        {
            string result = null;

            var response = await _client.GetAsync(Request());

            using (response)
            {
                StreamReader sr = new StreamReader(response.Content.ReadAsStream());
                result = sr.ReadToEnd();
                Console.Write(result);
            }

            return JsonSerializer.Deserialize<Forecast>(result);
        }

        private static string GetLocationStringAsync()
        {
            var location = _windowsService.GetCurrentLocationAsync();
            var latitude = location.Latitude.ToString();
            var longitude = location.Longitude.ToString();

            return ($"?latitude={latitude}&longitude={longitude}");
        }

        private static Uri Request()
        {
            var location = GetLocationStringAsync();

            return new Uri(Constants.weatherBaseUri + Constants.weatherForecastRequest + location + Constants.weatherCurrentWeather + "&daily=sunrise,sunset" + "&timezone=auto");
        }
    }
}
