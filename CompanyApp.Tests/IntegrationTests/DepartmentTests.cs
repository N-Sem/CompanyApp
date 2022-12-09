using CompanyApp.Dal.Repo.Interfaces;
using CompanyApp.Dal.Repo;
using CompanyApp.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Tests.IntegrationTests
{
    [Collection("IntegtationTests")]
    public class DepartmentTests : BaseTest, IClassFixture<EnsureCompanyDatabaseTextFixture>
    {
        [Fact]
        public void CanGetAllEmployeesInDepartment()
        {
            IDepartmentRepo repo = new DepartmentRepo(this.Context);
            var entities = repo.GetAllEmployeesInDepartment(1);

            Assert.NotEmpty(entities);
            Assert.Equal(3, entities.Count());

            repo.Dispose();
        }
        
        [Fact]
        public void CanGetDepartmentDirector()
        {
            IDepartmentRepo repo = new DepartmentRepo(this.Context);
            var entity = repo.GetDepartmentDirector(1);

            Assert.NotNull(entity);
            Assert.Equal(1, entity.Id);

            repo.Dispose();
        }
    }
}
