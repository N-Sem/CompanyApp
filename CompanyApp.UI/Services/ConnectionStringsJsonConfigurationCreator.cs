using CompanyApp.UI.Services.AppSettingsJsonUpdaterTemplates;
using System.Text.Json;

namespace CompanyApp.UI.Services
{
    public static class ConnectionStringsJsonConfigurationCreator
    {
        public static string CreateConfiguration(string connString)
        {
            ConnectionStringsKey settings = new ConnectionStringsKey(new ConnectionStringsValue(connString));
            return JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = false });
        }
    }
}
