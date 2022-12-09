using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Dal.EfStructures
{
    public class DesignTimeApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = @"server=localhost;port=3306;database=company;uid=root;pwd=root";
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version()));
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
