using CompanyApp.Dal.Repo.Base;
using CompanyApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Dal.Repo.Interfaces
{
    public interface IOrderRepo : IRepo<Order>
    {
        IEnumerable<Employee> GetAllEmployeesWhoHasOrder(int orderId);

        IEnumerable<Order> GetAllEmployeeOrders(int employeeId);
    }
}
