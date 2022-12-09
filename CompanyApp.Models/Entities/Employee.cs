using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CompanyApp.Enums;
using CompanyApp.Models.Base;

namespace CompanyApp.Models.Entities
{
    public class Employee : BaseEntity
    {
        [Required, StringLength(50)]
        public string? FirstName { get; set; }

        [Required, StringLength(50)]
        public string? MiddleName { get; set; }

        [Required, StringLength(50)]
        public string? LastName { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }

        public Gender? Gender { get; set; }

        public int? DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department? DepartmentNavigation { get; set; }

        public bool IsDirector { get; set; }

        [InverseProperty(nameof(Order.Employees))]
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
    }
}
