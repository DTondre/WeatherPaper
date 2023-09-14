using WeatherPaper.Models;

namespace WeatherPaper.Services.Interfaces
{
    public interface IWallpaperService
    {
        Task<string> GetWallpaperAsync(Forecast forecast);
        string GetDayQuery(Forecast forecast);
    }
}
