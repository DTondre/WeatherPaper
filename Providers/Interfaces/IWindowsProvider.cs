using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPaper.Providers.Interfaces
{
    public interface IWindowsProvider
    {
        Task SetWallpaperAsync(string imgPath);
    }
}
