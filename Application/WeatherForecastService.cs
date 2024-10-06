using System.Collections.Generic;
using Domain; 
using Persistence; // This should include your DbContext
using System.Linq; 

namespace Application
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly DataContext _context; 

        public WeatherForecastService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<WeatherForecast> GetAll()
        {
            return _context.WeatherForecasts.ToList(); // Ensure this uses the DbSet from DataContext
        }

        public void Add(WeatherForecast forecast)
        {
            _context.WeatherForecasts.Add(forecast);
            _context.SaveChanges();
        }

        
    }
}
