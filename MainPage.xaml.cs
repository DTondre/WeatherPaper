using WeatherPaper.Services;

namespace WeatherPaper
{
    public partial class MainPage : ContentPage
    {
        readonly WeatherService _weatherService = new();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            DisplayLabel.Text = await _weatherService.GetWeatherInfoAsync();
        }
    }
}