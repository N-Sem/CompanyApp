using CompanyApp.Dal.EfStructures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CompanyApp.Tests
{
    public class TestHelpers
    {
        public static IConfiguration GetConfiguration() =>
            new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();

        public static ApplicationDbContext GetContext(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("company");
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version()));
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
