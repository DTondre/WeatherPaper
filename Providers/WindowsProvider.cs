using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Devices;
using Microsoft.Win32;
using WeatherPaper.Providers.Interfaces;
using Windows.Storage;
using Windows.System.UserProfile;

namespace WeatherPaper.Services
{
    public class WindowsProvider : IWindowsProvider
    {
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation = false;
        private bool _isUpdatingWallpaper = false;

        public async Task<Location> GetCurrentLocationAsync()
        {
            Location location = new();

            if (!_isCheckingLocation)
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                _cancelTokenSource = new CancellationTokenSource();

                location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                _isCheckingLocation = false;
            }
            return location;
        }

        public async Task SetWallpaperAsync(string imgPath)
        {
            _isUpdatingWallpaper = true;

            StorageFile file = await StorageFile.GetFileFromPathAsync(imgPath);

            UserProfilePersonalizationSettings profileSettings = UserProfilePersonalizationSettings.Current;
            await profileSettings.TrySetWallpaperImageAsync(file);

            _isUpdatingWallpaper = false;
        }

        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            {
                _cancelTokenSource.Cancel();
            }
        }


    }
}
