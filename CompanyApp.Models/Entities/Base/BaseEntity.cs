using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Models.Base
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Timestamp]
        public byte[]? TimeStamp { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
