using CompanyApp.Dal.EfStructures;
using CompanyApp.Dal.Repo.Base;
using CompanyApp.Dal.Repo.Interfaces;
using CompanyApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyApp.Dal.Repo
{
    public class OrderRepo : BaseRepo<Order>, IOrderRepo
    {
        public OrderRepo(ApplicationDbContext context) : base(context)
        {
        }
        internal OrderRepo(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public IEnumerable<Employee> GetAllEmployeesWhoHasOrder(int orderId) =>
            (Table.Include(o => o.Employees).Where(o => o.Id == orderId).FirstOrDefault() ?? new())
                .Employees;

        public IEnumerable<Order> GetAllEmployeeOrders(int employeeId)
        {
            var res = Table.Include(o => o.Employees).Where(o => o.Employees.Any(e => e.Id == employeeId));
            return res;
        }
    }
}
