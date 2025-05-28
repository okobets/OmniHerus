using Microsoft.AspNetCore.Mvc;
using Web.Server.Models;
using Web.Server.Services;

namespace Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(WeatherCollectionService weatherCollectionService) : ControllerBase
    {
        private readonly WeatherCollectionService weatherCollectionService = weatherCollectionService;

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return weatherCollectionService.GetWeatherForecasts();
        }
    }
}
