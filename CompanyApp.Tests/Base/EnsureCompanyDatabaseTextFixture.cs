using CompanyApp.Dal.Initialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
