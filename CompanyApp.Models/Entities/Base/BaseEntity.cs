using System.ComponentModel.DataAnnotations;

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
