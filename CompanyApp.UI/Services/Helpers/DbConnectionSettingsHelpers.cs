using CompanyApp.Dal.EfStructures;
using CompanyApp.Dal.Initialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CompanyApp.UI.Services.Helpers
{
    public static class DbConnectionSettingsHelpers
    {
        public static string CurrentConnectionString =>
            new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build()
            .GetConnectionString("company");

        public static (bool Result, string Message) UpdateConnectionStringsAppSetting(string newConnString)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            var updatedConfigJson = ConnectionStringsJsonConfigurationCreator.CreateConfiguration(newConnString);
            try
            {
                File.WriteAllText(filePath, updatedConfigJson);
                return (true, $"Строка подключения {newConnString} сохранена");
            }
            catch (Exception ex)
            {
                return (false, $"Ошибка сохранения строки подключения {newConnString}. Сообщение: {ex.Message}");
            }
        }

        private static ApplicationDbContext? GetDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version()));
            try
            {
                return new ApplicationDbContext(optionsBuilder.Options);
            }
            catch
            {
                return null;
            }
        }

        public static (bool Result, string Message) TestConnectionToMySqlServer(string connectionString)
        {
            try
            {                
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    return (true, "Подключение выполнено успешно");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Ошибка подключения к базе данных: {ex.Message}");
            }
        }

        public static (bool Result, string Message) DropAndCreateDatabase(string connectionString)
        {
            using (var context = GetDbContext(connectionString))
            {
                try
                {
                    SampleDatalnitializer.DropAndCreateDatabase(context);
                    return (true, "База данных создана успешно");
                }
                catch (Exception ex)
                {
                    return (false, $"Ошибка создания базы данных. Сообщение: {ex.Message}");
                }
            }
        }

        public static (bool Result, string Message) CreateDatabaseAndAddSeedData(string connectionString)
        {
            using (var context = GetDbContext(connectionString))
            {
                try
                {
                    SampleDatalnitializer.InitializeData(context);
                    return (true, "База данных создана успешно");
                }
                catch (Exception ex)
                {
                    return (false, $"Ошибка создания базы данных. Сообщение: {ex}");
                }
            }
        }
    }
}
