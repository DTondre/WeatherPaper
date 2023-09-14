using System.Security.Cryptography;
using System.Text.Json;
using WeatherPaper.Models;
using WeatherPaper.Services.Interfaces;
using Windows.Storage;

namespace WeatherPaper.Services
{
    public class WallpaperService : IWallpaperService
    {
        private static readonly HttpClient _client = new HttpClient()
        {
            BaseAddress = new Uri(Constants.wallpaperBaseUri)

        };

        public async Task<string> GetWallpaperAsync(Forecast forecast)
        {
            string result = null;

            var response = await _client.GetAsync(Request(forecast));

            using (response)
            {
                StreamReader sr = new(response.Content.ReadAsStream());
                result = sr.ReadToEnd();
                Console.Write(result);
            }

            var wallpaperResults = JsonSerializer.Deserialize<IEnumerable<Results>>(result);
            var wallpaper = wallpaperResults.ElementAt(RandomNumberGenerator.GetInt32(9));
            var url = wallpaper.urls.raw;
            var uri = new Uri(url);
            var fileName = wallpaper.id;

            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

            var httpResult = await _client.GetAsync(uri);
            using var resultStream = await httpResult.Content.ReadAsStreamAsync();
            using var fileStream = File.Create(storageFolder.Path + "\\" + fileName);
            resultStream.CopyTo(fileStream);

            Uri trackDownloadUri = new Uri(wallpaper.links.download_location + "&client_id=2RfrPF5cZPBQYi4_rNxO2X4bcfyXmYVb56s0jtFhML0");

            await _client.GetAsync(wallpaper.links.download_location);

            return storageFolder.Path + "\\" + fileName;
        }

        public string Request(Forecast forecast)
        {
            string request = Constants.wallpaperBaseUri + "&query=wallpaper+nature+4k+" + GetDayQuery(forecast);
            
            return request;
        }

        public string GetDayQuery(Forecast forecast)
        {
            DateTime sunrise = forecast.daily.sunrise.First();
            DateTime sunset = forecast.daily.sunset.First();

            int sunriseDiff = DateTime.Now.Hour - sunrise.Hour;
            int sunsetDiff = DateTime.Now.Hour - sunset.Hour;

            if (sunriseDiff <= 1 && sunriseDiff >= -1)
            {
                return "sunrise";
            }
            if (sunsetDiff <= 1 && sunsetDiff >= -1)
            {
                return "sunset";
            }
            if (DateTime.Now.Hour <= sunrise.Hour || DateTime.Now.Hour >= sunset.Hour)
            {
                return "night";
            }
            else
            {
                return "sunny";
            }
        }
    }
}
