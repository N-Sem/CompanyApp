using CompanyApp.Dal.Repo.Interfaces;
using CompanyApp.Dal.Repo;
using CompanyApp.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyApp.Models;

namespace CompanyApp.Tests.IntegrationTests
{
    [Collection("IntegtationTests")]
    public class OrderTests : BaseTest, IClassFixture<EnsureCompanyDatabaseTextFixture>
    {
        [Fact]
        public void CanGetAllEmployeeWhoHasOrder()
        {
            IOrderRepo repo = new OrderRepo(this.Context);
            var entities = repo.GetAllEmployeesWhoHasOrder(4);

            Assert.NotEmpty(entities);
            Assert.Equal(1, entities.Count());

            repo.Dispose();
        }

        [Fact]
        public void CanGetAllEmployeeOrders()
        {
            IOrderRepo repo = new OrderRepo(this.Context);
            var entities = repo.GetAllEmployeeOrders(1);
            var entities2 = repo.GetAllEmployeeOrders(2);

            Assert.Empty(entities2);
            Assert.NotEmpty(entities);
            Assert.Equal(3, entities.Count());

            repo.Dispose();
        }
    }
}
