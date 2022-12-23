using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CompanyApp.Models.Base;

namespace CompanyApp.Models.Entities
{
    public class Department : BaseEntity
    {
        [Required]
        public string? Name { get; set; }

        [InverseProperty(nameof(Employee.DepartmentNavigation))]
        public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
    }
}
