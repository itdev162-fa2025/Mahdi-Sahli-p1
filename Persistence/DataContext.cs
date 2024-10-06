using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public string DbPath { get; }

        // Constructor for dependency injection
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
           
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "Blogbox.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            
            if (!options.IsConfigured)
            {
                options.UseSqlite($"Data Source={DbPath}");
            }
        }
    }
}
