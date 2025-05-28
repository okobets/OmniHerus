using Web.Server.Models;

namespace Web.Server.Services
{
    public class WeatherCollectionService(ILogger<WeatherCollectionService> logger)
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];
        private readonly ILogger<WeatherCollectionService> _logger = logger;

        public IEnumerable<WeatherForecast> GetWeatherForecasts()
        {
            _logger.LogInformation("Generating weather forecasts at {Time}", DateTime.Now);
            return [.. Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })];
        }
    }
}
