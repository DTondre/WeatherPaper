namespace WeatherPaper.Models
{
    public class Daily
    {
        public IEnumerable<DateTime> sunrise { get; set; }
        public IEnumerable<DateTime> sunset { get; set; }
    }
}
