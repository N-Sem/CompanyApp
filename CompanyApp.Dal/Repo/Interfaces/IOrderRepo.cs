using CompanyApp.Dal.Repo.Base;
using CompanyApp.Models.Entities;

namespace CompanyApp.Dal.Repo.Interfaces
{
    public interface IOrderRepo : IRepo<Order>
    {
        IEnumerable<Employee> GetAllEmployeesWhoHasOrder(int orderId);

        IEnumerable<Order> GetAllEmployeeOrders(int employeeId);
    }
}
