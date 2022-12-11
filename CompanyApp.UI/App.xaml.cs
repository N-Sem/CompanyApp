using CompanyApp.Dal.EfStructures;
using CompanyApp.Dal.Repo;
using CompanyApp.Dal.Repo.Interfaces;
using CompanyApp.Models.Entities;
using CompanyApp.UI.Pages;
using CompanyApp.UI.Services.ShareEntity;
using CompanyApp.UI.ViewModels;
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
    public partial class App
    {
        public static IHost AppHost => App.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args);

            hostBuilder.UseContentRoot(Environment.CurrentDirectory);
            hostBuilder.ConfigureAppConfiguration((host, cfg) =>
            {
                cfg.SetBasePath(Environment.CurrentDirectory);
                cfg.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            });

            hostBuilder.ConfigureServices(App.ConfigureServices);

            return hostBuilder;
        }

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            string connectionString = host.Configuration.GetConnectionString("company");

            services.AddDbContext<ApplicationDbContext>(opts =>
            {
                opts.UseMySql(connectionString, new MySqlServerVersion(new Version()));
            });

            services.AddSingleton<EmployeesWindow>();
            services.AddSingleton<EditEmployeeWindow>();

            services.AddSingleton<EmployeesWindowViewModel>();
            services.AddSingleton<EditEmployeeWindowViewModel>();

            services.AddSingleton<IEmployeeRepo, EmployeeRepo>();
            services.AddSingleton<IDepartmentRepo, DepartmentRepo>();
            services.AddSingleton<IOrderRepo, OrderRepo>();

            services.AddSingleton<ISharedEntity<Employee>, SharedEmployee>();
            services.AddSingleton<ISharedEntity<int?>, SharedNullableIntegerId>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync().ConfigureAwait(true);

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync().ConfigureAwait(false);
            AppHost.Dispose();

            base.OnExit(e);
        }
    }
}
