using WeatherPaper.Services;

namespace WeatherPaper
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            var results = await WeatherService.GetWeatherInfoAsync();

            var IsDay = results.current_weather.is_day != 0 ? "day" : "night";

            TemperatureLabel.Text = $"It is currently {results.current_weather.temperature} degrees celsius.";
            IsDayLabel.Text = $"It is currently {IsDay}.";

            await WallpaperService.ChangeWallpaperAsync();
        }
    }
}