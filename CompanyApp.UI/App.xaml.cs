using CompanyApp.Dal.EfStructures;
using CompanyApp.Dal.Repo;
using CompanyApp.Dal.Repo.Interfaces;
using CompanyApp.UI.Pages;
using CompanyApp.UI.Services.ShareEntity;
using CompanyApp.UI.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace CompanyApp.UI
{
    public partial class App
    {
        public static IHost AppHost => App.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args);

            hostBuilder.UseContentRoot(Environment.CurrentDirectory);
            hostBuilder.ConfigureAppConfiguration((host, conf) =>
            {                
                conf.SetBasePath(Environment.CurrentDirectory);
                conf.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            });

            hostBuilder.ConfigureServices(App.ConfigureServices);

            return hostBuilder;
        }

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            string? connectionString = host.Configuration.GetConnectionString("company");

            if (connectionString == null || connectionString?.Trim().Length < 10)
                connectionString = "server=localhost;port=3306;";

            services.AddDbContext<ApplicationDbContext>(opts =>
            {
                opts.UseMySql(connectionString, new MySqlServerVersion(new Version()));
            });

            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<EditEmployeeWindowViewModel>();
            services.AddSingleton<EditDepartmentWindowViewModel>();
            services.AddSingleton<EditOrderWindowViewModel>();

            services.AddSingleton<EmployeesPageViewModel>();

            services.AddSingleton<DepartmentsPageViewModel>();

            services.AddSingleton<OrdersPageViewModel>();

            services.AddSingleton<SettingsPageViewModel>();
           
            services.AddTransient<IEmployeeRepo, EmployeeRepo>();
            services.AddTransient<IDepartmentRepo, DepartmentRepo>();
            services.AddTransient<IOrderRepo, OrderRepo>();

            services.AddSingleton<ISharedEntity<int?>, SharedNullableIntegerId>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            var cultureInfo = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
            
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
