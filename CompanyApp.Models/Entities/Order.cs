using CompanyApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Models.Entities
{
    public class Order : BaseEntity
    {
        [Required]
        public string? Name { get; set; }

        [InverseProperty(nameof(Employee.Orders))]
        public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
    }
}
