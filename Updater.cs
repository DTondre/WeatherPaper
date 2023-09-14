using System.Diagnostics;
using System.Timers;
using WeatherPaper.Services.Interfaces;
using WeatherPaper.Services;

namespace WeatherPaper
{
    public class Updater
    {
        private static System.Timers.Timer _updateTimer;
        private static string _lastImgPath;
        private static bool _isUpdating = false;

        private static readonly IWallpaperService _wallpaperProvider = new WallpaperService();
        private static readonly IWeatherService _weatherProvider = new WeatherService();
        private static readonly IWindowsService _windowsProvider = new WindowsService();
        
        public Updater()
        {
            UpdateWallpaperAsync();
            SetTimer();
        }

        private static void SetTimer()
        {
            _updateTimer = new System.Timers.Timer(TimeSpan.FromSeconds(60));

            _updateTimer.Elapsed += OnTimedEvent;
            _updateTimer.AutoReset = false;
            _updateTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            UpdateWallpaperAsync();
            Debug.WriteLine($"Updated wallpaper at {DateTime.Now}");
            SetTimer();
        }

        private static async void UpdateWallpaperAsync()
        {
            if (!_isUpdating)
            {
                _isUpdating = true;
                var weatherInfo = await _weatherProvider.GetWeatherInfoAsync();
                var imgPath = await _wallpaperProvider.GetWallpaperAsync(weatherInfo);
                await _windowsProvider.SetWallpaperAsync(imgPath);

                if (_lastImgPath != null && File.Exists(_lastImgPath))
                {
                    File.Delete(_lastImgPath);
                }
                _lastImgPath = imgPath;
                _isUpdating = false;
            }
        }
    }
}
