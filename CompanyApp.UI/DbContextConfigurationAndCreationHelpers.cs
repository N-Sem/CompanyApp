using CompanyApp.Dal.EfStructures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.UI
{
    public class DbContextConfigurationAndCreationHelpers
    {
        //public static IConfiguration GetConfiguration() =>
        //    new ConfigurationBuilder()
        //    .SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("appsettings.json", true, true)
        //    .Build();

        public static ApplicationDbContext GetContext(/*IConfiguration configuration*/)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = @"server=localhost;port=3306;database=company;uid=root;pwd=root";
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version()));
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
