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
    public class DepartmentRepo : BaseRepo<Department>, IDepartmentRepo
    {
        public DepartmentRepo(ApplicationDbContext context) : base(context)
        {
        }
        internal DepartmentRepo(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public IEnumerable<Employee>? GetAllEmployeesInDepartment(int departmentId) =>
            (Table.Where(d => d.Id == departmentId).Include(d => d.Employees).FirstOrDefault() ?? new())
            .Employees;

        public Employee? GetDepartmentDirector(int departmentId) =>
            (Table.Where(d => d.Id == departmentId).Include(d => d.Employees).FirstOrDefault() ?? new())
            .Employees
            .Where(e => e.IsDirector)
            .FirstOrDefault();
    }
}
