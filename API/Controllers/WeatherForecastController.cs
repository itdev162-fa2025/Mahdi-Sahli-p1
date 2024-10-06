using Domain;
using Microsoft.AspNetCore.Mvc;
using Application; // Make sure to include this to access your service
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _service; // Use the service instead of DataContext

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _logger = logger;
            _service = service; // Inject the service
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            // You can also choose to fetch forecasts from the database using the service
            // var forecasts = _service.GetAll(); 

            return Ok(forecasts);
        }

        [HttpPost]
        public ActionResult<WeatherForecast> Create([FromBody] WeatherForecast forecast) // Accept the forecast from the request body
        {
            if (forecast == null)
            {
                return BadRequest("Forecast cannot be null.");
            }

            // Using the service to add the forecast
            _service.Add(forecast);

            return CreatedAtAction(nameof(Get), new { id = forecast.Id }, forecast);
        }
    }
}
