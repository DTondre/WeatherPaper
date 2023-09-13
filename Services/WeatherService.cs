using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherPaper.Models;

namespace WeatherPaper.Services
{
    public class WeatherService
    {
        private static readonly HttpClient _client = new HttpClient()
        {
            BaseAddress = new Uri(Constants.weatherBaseUri)
        };
        static readonly WindowsService _windowsService = new WindowsService();

        public static async Task<Forecast> GetWeatherInfoAsync()
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

        static async Task<String> GetLocationStringAsync()
        {
            var location = await _windowsService.GetCurrentLocationAsync();
            var latitude = location.Latitude.ToString();
            var longitude = location.Longitude.ToString();

            return ($"?latitude={latitude}&longitude={longitude}");
        }

        static async Task<Uri> Request()
        {
            var location = await GetLocationStringAsync();

            return new Uri(Constants.weatherBaseUri + Constants.weatherForecastRequest + location + Constants.weatherCurrentWeather);
        }
    }
}
