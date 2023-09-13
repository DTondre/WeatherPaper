using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ABI.System;
using Microsoft.Maui.Devices;
using Microsoft.Win32;
using Windows.Storage;
using Windows.System.UserProfile;

namespace WeatherPaper.Services
{
    public enum Style
    {
        Tiled,
        Centered,
        Stretched
    }

    public class WindowsService
    {
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x0002;

        public async Task<Location> GetCurrentLocationAsync()
        {

            _isCheckingLocation = true;

            GeolocationRequest request = new(GeolocationAccuracy.Medium, System.TimeSpan.FromSeconds(10));

            _cancelTokenSource = new CancellationTokenSource();

            Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

            if (location != null)
                return location;

            _isCheckingLocation = false;

            return null;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(UInt32 uiAction, UInt32 uiParam, IntPtr pvParam, UInt32 fWinIni);

        public static async void SetWallpaperAsync(Style style)
        {
            string imgPath = await WallpaperService.GetWallpaperAsync();
            
            StorageFile file = await StorageFile.GetFileFromPathAsync(imgPath);

            UserProfilePersonalizationSettings profileSettings = UserProfilePersonalizationSettings.Current;
            await profileSettings.TrySetWallpaperImageAsync(file);
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
