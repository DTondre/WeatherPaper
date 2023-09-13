using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherPaper.Providers.Interfaces;
using WeatherPaper.Services;

namespace WeatherPaper
{
    public class Updater
    {
        private readonly IWallpaperProvider _wallpaperProvider = new WallpaperProvider();
        private readonly IWeatherProvider _weatherProvider = new WeatherProvider();
        private readonly IWindowsProvider _windowsProvider = new WindowsProvider();

        public async void UpdateWallpaperAsync()
        {
            var imgPath = await _wallpaperProvider.GetWallpaperAsync();
            await _windowsProvider.SetWallpaperAsync(imgPath);
        }
    }
}
