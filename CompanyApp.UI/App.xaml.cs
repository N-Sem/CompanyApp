using CompanyApp.Dal.EfStructures;
using CompanyApp.Dal.Repo;
using CompanyApp.Dal.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CompanyApp.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost? _host;
        public static IHost Host => _host ?? Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            string connectionString = host.Configuration.GetConnectionString("company");

            services.AddDbContext<ApplicationDbContext>(opts =>
            {
                opts.UseMySql(connectionString, new MySqlServerVersion(new Version()));
            });

            services.AddSingleton<IEmployeeRepo, EmployeeRepo>();
            services.AddSingleton<IDepartmentRepo, DepartmentRepo>();
            services.AddSingleton<IOrderRepo, OrderRepo>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await Host.StartAsync().ConfigureAwait(false);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            await Host.StopAsync().ConfigureAwait(false);
            Host.Dispose();
        }
    }
}
