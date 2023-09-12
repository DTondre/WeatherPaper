using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Devices;

namespace WeatherPaper.Services
{
    public class WindowsService
    {
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        public async Task<Location> GetCurrentLocationAsync()
        {

            _isCheckingLocation = true;

            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            _cancelTokenSource = new CancellationTokenSource();

            Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

            if (location != null)
                return location;

            _isCheckingLocation = false;

            return null;
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
