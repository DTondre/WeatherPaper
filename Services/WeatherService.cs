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
            BaseAddress = new Uri("https://api.open-meteo.com/v1/")
        };
        static readonly WindowsService _windowsService = new WindowsService();

        public async Task<string> GetWeatherInfoAsync()
        {
            WebRequest request = WebRequest.Create(await Request());
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();

            string result = null;
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd();
                Console.Write(result);
            }

            Forecast? forecast = JsonSerializer.Deserialize<Forecast>(result);

            return result;
        }

        static async Task<String> GetLocationStringAsync()
        {
            var location = await _windowsService.GetCurrentLocationAsync();
            var latitude = location.Latitude.ToString();
            var longitude = location.Longitude.ToString();

            return ($"?latitude={latitude}&longitude={longitude}");
        }

        static async Task<string> Request()
        {
            var location = await GetLocationStringAsync();

            return String.Format(Constants.weatherBaseUri + Constants.weatherForecastRequest + location + Constants.weatherCurrentWeather);
        }
    }
}
