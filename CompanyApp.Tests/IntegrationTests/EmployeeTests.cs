using CompanyApp.Dal.Repo;
using CompanyApp.Dal.Repo.Interfaces;
using CompanyApp.Models.Entities;
using CompanyApp.Tests.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CompanyApp.Tests.IntegrationTests
{
    [Collection("IntegtationTests")]
    public class EmployeeTests : BaseTest, IClassFixture<EnsureCompanyDatabaseTextFixture>
    {
        static int itemsCount = 11;
        [Fact]
        public void CanAddEntity()
        {
            IEmployeeRepo repo = new EmployeeRepo(this.Context);

            ExecuteInATransaction(RunTheTest);

            void RunTheTest()
            {
                var entity = new Employee
                {
                    FirstName = "NewFirstName",
                    MiddleName = "NewMiddleName",
                    LastName = "NewLastName",
                    BirthDate = DateTime.Now
                };
                repo.Add(entity);

                Assert.Equal(itemsCount + 1, Context.Employees.Count());
            }
            repo.Dispose();
        }

        [Fact]
        public void CanAddRange()
        {
            IEmployeeRepo repo = new EmployeeRepo(this.Context);

            ExecuteInATransaction(RunTheTest);

            void RunTheTest()
            {
                var entitiesToAdd = new List<Employee>()
                {
                    new() { FirstName = "First_FirstName", MiddleName = "First_MiddleName", LastName = "First_LastName", BirthDate = DateTime.Now },
                    new() { FirstName = "Second_FirstName", MiddleName = "Second_MiddleName", LastName = "Second_LastName", BirthDate = DateTime.Now },
                    new() { FirstName = "Third_FirstName", MiddleName = "Third_MiddleName", LastName = "Third_LastName", BirthDate = DateTime.Now }
                };
                repo.AddRange(entitiesToAdd, true);

                Assert.Equal(itemsCount + 3, Context.Employees.Count());
            }
            repo.Dispose();
        }

        [Fact]
        public void CanDeleteById()
        {
            IEmployeeRepo repo = new EmployeeRepo(this.Context);

            ExecuteInATransaction(RunTheTest);

            void RunTheTest()
            {
                repo.Delete(2, new byte[1]);

                Assert.Null(Context.Employees.Where(e=>e.Id==2).FirstOrDefault());
                Assert.Equal(itemsCount - 1, Context.Employees.Count());
            }
            repo.Dispose();
        }

        [Fact]
        public void CanDeleteEntity()
        {
            IEmployeeRepo repo = new EmployeeRepo(this.Context);

            ExecuteInATransaction(RunTheTest);

            void RunTheTest()
            {
                var entityToDelete = Context.Employees.Find(2);
                repo.Delete(entityToDelete!);

                Assert.Null(Context.Employees.Where(e => e.Id == 2).FirstOrDefault());
                Assert.Equal(itemsCount - 1, Context.Employees.Count());
            }
            repo.Dispose();
        }

        [Fact]
        public void CanDeleteRange()
        {
            IEmployeeRepo repo = new EmployeeRepo(this.Context);

            ExecuteInATransaction(RunTheTest);

            void RunTheTest()
            {
                var entities = Context.Employees;
                var entitiesCount = entities.Count();
                var entitiesToDelete = entities.Select(e => e).Take(2);
                repo.DeleteRange(entitiesToDelete, true);
                var newEmpsCount = Context.Employees.Count();

                Assert.Equal(itemsCount - 2, Context.Employees.Count());
            }
            repo.Dispose();
        }

        [Fact]
        public void CanFindById()
        {
            IEmployeeRepo repo = new EmployeeRepo(this.Context);
            var entity = repo.Find(1) ?? new Employee();

            Assert.Equal("Михаил", entity.FirstName);

            repo.Dispose();
        }

        [Fact]
        public void CanGetAllByDepartment()
        {
            IEmployeeRepo repo = new EmployeeRepo(this.Context);
            var entities = repo.GetAllByDepartment(1);

            Assert.Equal(3, entities.Count());

            repo.Dispose();
        }

        [Fact]
        public void CanGetAllEmployeesWhoHasOrder()
        {
            IEmployeeRepo repo = new EmployeeRepo(this.Context);
            var entities = repo.GetAllEmployeesWhoHasOrder(5);

            Assert.Equal(2, entities.Count());

            repo.Dispose();
        }

        [Fact]
        public void CanGetAllEmployeeOrders()
        {
            IEmployeeRepo repo = new EmployeeRepo(this.Context);
            var entities = repo.GetAllEmployeeOrders(5);

            Assert.Equal(3, entities.Count());

            repo.Dispose();
        }

        [Fact]
        public void CanGetAllEntities()
        {
            IEmployeeRepo repo = new EmployeeRepo(this.Context);
            var entities = repo.GetAll();

            Assert.Equal(itemsCount, entities.Count());

            repo.Dispose();
        }

        [Fact]
        public void CanGetDepartmentDirector()
        {
            IEmployeeRepo repo = new EmployeeRepo(this.Context);
            var entity = repo.GetDepartmentDirector(2);

            Assert.NotNull(entity);
            Assert.Equal(4, entity!.Id);

            repo.Dispose();
        }

        [Fact]
        public void CanSetDepartmentDirector()
        {
            IEmployeeRepo repo = new EmployeeRepo(this.Context);

            ExecuteInATransaction(RunTheTest);

            void RunTheTest()
            {
                repo.SetDepartmentDirector(2, 5, true);

                Assert.Equal(5, Context.Employees.Where(e => e.IsDirector && e.DepartmentId == 2).Select(e => e.Id).FirstOrDefault());
                Assert.False(Context.Employees.Find(4)!.IsDirector);
            }
            repo.Dispose();
        }

        [Fact]
        public void CanUpdateEntity()
        {
            IEmployeeRepo repo = new EmployeeRepo(this.Context);

            ExecuteInATransaction(RunTheTest);

            void RunTheTest()
            {
                var entity = repo.Find(1);
                entity!.FirstName = "Максим";
                repo.Update(entity, true);

                Assert.Equal("Максим", Context.Employees.Find(1)!.FirstName);
            }
            repo.Dispose();
        }

        [Fact]
        public void CanUpdateRange()
        {
            IEmployeeRepo repo = new EmployeeRepo(this.Context);

            ExecuteInATransaction(RunTheTest);

            void RunTheTest()
            {
                var entities = repo.GetAll().Take(2).ToList();
                entities[0].FirstName = "Максим";
                entities[1].FirstName = "Елена";
                repo.UpdateRange(entities, true);

                var entitiesAfterUpdate = Context.Employees.OrderBy(e=>e.Id).Take(2).ToList();

                Assert.Equal("Максим", entitiesAfterUpdate[0].FirstName);
                Assert.Equal("Елена", entitiesAfterUpdate[1].FirstName);
            }
            repo.Dispose();
        }
    }
}
