using CompanyApp.Dal.Repo.Base;
using CompanyApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Dal.Repo.Interfaces
{
    public interface IEmployeeRepo : IRepo<Employee>
    {
        IEnumerable<Employee> GetAllByDepartment(int departmentId);

        IEnumerable<Employee> GetAllEmployeesWhoHasOrder(int orderId);

        IEnumerable<Order> GetAllEmployeeOrders(int employeeId);

        Employee? GetDepartmentDirector(int departmentId);

        int SetDepartmentDirector(int departmentId, int employeeId, bool persist = true);
    }
}
