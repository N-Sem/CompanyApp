namespace CompanyApp.UI.Services.AppSettingsJsonUpdaterTemplates
{
    public class ConnectionStringsKey
    {
        public ConnectionStringsValue ConnectionStrings { get; set; }

        public ConnectionStringsKey(ConnectionStringsValue dbConnectionStringSettings)
        {
            ConnectionStrings = dbConnectionStringSettings;
        }

    }
}
