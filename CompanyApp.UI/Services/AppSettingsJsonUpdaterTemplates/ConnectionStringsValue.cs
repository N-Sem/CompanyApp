namespace CompanyApp.UI.Services.AppSettingsJsonUpdaterTemplates
{
    public class ConnectionStringsValue
    {
        public string company { get; set; }
        public ConnectionStringsValue(string company)
        {
            this.company = company;
        }
    }
}
