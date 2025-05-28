using Web.Server.Controllers;
using Moq;
using Web.Server.Services;
using Web.Server.Models;

namespace Web.Server.Tests
{
    [TestClass]
    public sealed class WeatherCollectionTests
    {
        [TestMethod]
        public void GetForecast()
        {
            var loggerMock = new Mock<Microsoft.Extensions.Logging.ILogger<WeatherCollectionService>>();
            var weatherCollectionService = new WeatherCollectionService(loggerMock.Object);
            var controller = new WeatherForecastController(weatherCollectionService);
            
            var result = controller.Get();
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<IEnumerable<WeatherForecast>>(result);
            Assert.IsTrue(result.Any(), "The weather forecast collection should not be empty.");
        }
    }
}
