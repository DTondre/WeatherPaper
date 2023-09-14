namespace WeatherPaper.Models
{
    public class Forecast
    {
        public string timezone { get; set; }
        public CurrentWeather current_weather { get; set; }
        public Daily daily { get; set; }

    }
}
