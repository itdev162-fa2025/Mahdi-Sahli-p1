using Domain;
namespace Application
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> GetAll();
        void Add(WeatherForecast forecast);
        // Add other methods as needed (e.g., Update, Delete)
    }
}
