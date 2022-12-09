using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
