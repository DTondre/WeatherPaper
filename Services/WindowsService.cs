using WeatherPaper.Services.Interfaces;
using Windows.Storage;
using Windows.System.UserProfile;

namespace WeatherPaper.Services
{
    public class WindowsService : IWindowsService
    {
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation = false;
        private bool _isUpdatingWallpaper = false;

        public Location GetCurrentLocationAsync()
        {
            Location location = new();

            if (!_isCheckingLocation)
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                _cancelTokenSource = new CancellationTokenSource();

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);
                });

                _isCheckingLocation = false;
            }
            return location;
        }

        public async Task SetWallpaperAsync(string imgPath)
        {
            if (!_isUpdatingWallpaper)
            {
                _isUpdatingWallpaper = true;

                StorageFile file = await StorageFile.GetFileFromPathAsync(imgPath);

                UserProfilePersonalizationSettings profileSettings = UserProfilePersonalizationSettings.Current;
                await profileSettings.TrySetWallpaperImageAsync(file);

                _isUpdatingWallpaper = false;
            }
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
