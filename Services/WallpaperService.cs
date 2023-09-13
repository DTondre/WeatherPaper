using CommunityToolkit.Maui.Storage;
using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherPaper.Models;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace WeatherPaper.Services
{
    public class WallpaperService
    {
        private static readonly HttpClient _client = new HttpClient()
        {
            BaseAddress = new Uri(Constants.wallpaperBaseUri)

        };

        public static async Task ChangeWallpaperAsync()
        {
            var wallpaperPath = await GetWallpaperAsync();

            WindowsService.SetWallpaperAsync(Style.Stretched);
        }

        public static async Task<string> GetWallpaperAsync()
        {
            string result = null;

            var response = await _client.GetAsync(Constants.wallpaperBaseUri);

            using (response)
            {
                StreamReader sr = new(response.Content.ReadAsStream());
                result = sr.ReadToEnd();
                Console.Write(result);
            }

            var wallpaperResults = JsonSerializer.Deserialize<WallpaperResults>(result);
            int fileNumber = RandomNumberGenerator.GetInt32(23);
            var url = wallpaperResults.data.ElementAt(fileNumber).path;
            var uri = new Uri(url);
            var fileName = url[(url.LastIndexOf('-') + 1)..];

            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

            var httpResult = await _client.GetAsync(uri);
            using var resultStream = await httpResult.Content.ReadAsStreamAsync();
            using var fileStream = File.Create(storageFolder.Path + "\\" + fileName);
            resultStream.CopyTo(fileStream);

            
            return storageFolder.Path + "\\" + fileName;
        }
    }
}
