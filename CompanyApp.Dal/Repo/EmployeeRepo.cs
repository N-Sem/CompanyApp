using CompanyApp.Dal.EfStructures;
using CompanyApp.Dal.Repo.Base;
using CompanyApp.Dal.Repo.Interfaces;
using CompanyApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Dal.Repo
{
    public class EmployeeRepo : BaseRepo<Employee>, IEmployeeRepo
    {
        public EmployeeRepo(ApplicationDbContext context) : base(context)
        {
        }
        internal EmployeeRepo(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public override IEnumerable<Employee> GetAll() =>
            Table
            .Include(e => e.DepartmentNavigation)
            .Include(e => e.Orders)
            .OrderBy(e => e.Id);

        public override IEnumerable<Employee> GetAllAsNoTracking() =>
            Table
            .Include(e => e.DepartmentNavigation)
            .Include(e => e.Orders)
            .OrderBy(e => e.Id)
            .AsNoTrackingWithIdentityResolution();

        public IEnumerable<Employee> GetAllByDepartment(int departmentId) =>
            Table
            .Where(e => e.DepartmentId == departmentId)
            .Include(e => e.Orders)
            .Include(e => e.DepartmentNavigation)
            .OrderBy(e => e.Id);

        public IEnumerable<Employee> GetAllEmployeesWhoHasOrder(int orderId) =>
            Table
            .Where(e=>e.Orders.Any(o => o.Id == orderId))
            .OrderBy(e => e.Id);

        public IEnumerable<Order> GetAllEmployeeOrders(int employeeId) =>
            (Table.Include(e => e.Orders).Where(e => e.Id == employeeId).FirstOrDefault() ?? new())
            .Orders;

        public override IEnumerable<Employee> GetAllIgnoreQueryFilters() =>
            Table
            .Include(e => e.DepartmentNavigation)
            .Include(e => e.Orders)
            .OrderBy(e => e.Id)
            .IgnoreQueryFilters();

        public Employee? GetDepartmentDirector(int departmentId) =>
            Table
            .Where(e => e.DepartmentId == departmentId && e.IsDirector)
            .FirstOrDefault();

        public int SetDepartmentDirector(int departmentId, int employeeId, bool persist = true)
        {
            var currentDirectors = Table.Where(e => e.DepartmentId == departmentId && e.IsDirector);
            var newDirector = Find(employeeId);

            if (newDirector is not null)
            {
                newDirector.IsDirector = true;
                newDirector.DepartmentId = departmentId;
                Update(newDirector, false);

                if (currentDirectors is not null)
                {
                    foreach (var currentDirector in currentDirectors)
                    {
                        currentDirector.IsDirector = false;
                        Update(currentDirector, false);
                    }
                }
            }
            else
            {
                return 0;
            }

            return persist ? SaveChanges() : 0;
        }
    }
}
