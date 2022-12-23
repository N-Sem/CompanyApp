using CompanyApp.Dal.Repo.Base;
using CompanyApp.Models.Entities;

namespace CompanyApp.Dal.Repo.Interfaces
{
    public interface IDepartmentRepo : IRepo<Department>
    {
        IEnumerable<Employee>? GetAllEmployeesInDepartment(int departmentId);

        Employee? GetDepartmentDirector(int departmentId);
    }
}
