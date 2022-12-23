using CompanyApp.Dal.Initialization;

namespace CompanyApp.Tests.Base
{
    internal class EnsureCompanyDatabaseTextFixture : IDisposable
    {
        public EnsureCompanyDatabaseTextFixture()
        {
            var configuration = TestHelpers.GetConfiguration();
            var context = TestHelpers.GetContext(configuration);
            SampleDatalnitializer.ClearAndReseedDatabase(context);
            context.Dispose();
        }

        public void Dispose()
        {
        }
    }
}
