using CompanyApp.Dal.EfStructures;
using CompanyApp.Dal.Repo;
using CompanyApp.Dal.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using TestWpfApp.Data;
using TestWpfApp.ViewModels;

namespace TestWpfApp;

public partial class App
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<ApplicationDbContext>(opts =>
                {
                    opts.UseMySql(@"server=localhost;port=3306;database=company;uid=root;pwd=root", new MySqlServerVersion(new Version()));
                });

                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainWindowViewModel>();
                services.AddTransient<IEmployeeRepo, EmployeeRepo>();
                services.AddTransient<IDataAccess, DataAccess>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
        startupForm.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();

        base.OnExit(e);
    }
}
