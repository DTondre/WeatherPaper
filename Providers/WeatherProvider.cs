using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherPaper.Models;
using WeatherPaper.Providers.Interfaces;

namespace WeatherPaper.Services
{
    public class WeatherProvider : IWeatherProvider
    {
        private static readonly HttpClient _client = new HttpClient()
        {
            BaseAddress = new Uri(Constants.weatherBaseUri)
        };
        static readonly WindowsProvider _windowsService = new WindowsProvider();

        public async Task<Forecast> GetWeatherInfoAsync()
        {
            string result = null;

            var response = await _client.GetAsync(await Request());

            using (response)
            {
                StreamReader sr = new StreamReader(response.Content.ReadAsStream());
                result = sr.ReadToEnd();
                Console.Write(result);
            }

            return JsonSerializer.Deserialize<Forecast>(result);
        }

        private static async Task<string> GetLocationStringAsync()
        {
            var location = await _windowsService.GetCurrentLocationAsync();
            var latitude = location.Latitude.ToString();
            var longitude = location.Longitude.ToString();

            return ($"?latitude={latitude}&longitude={longitude}");
        }

        private static async Task<Uri> Request()
        {
            var location = await GetLocationStringAsync();

            return new Uri(Constants.weatherBaseUri + Constants.weatherForecastRequest + location + Constants.weatherCurrentWeather);
        }
    }
}
