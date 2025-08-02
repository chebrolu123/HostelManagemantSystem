using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagemantSystem.Models
{
    public class EmployeeAttendence
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Employees")]
        public int EmployeeId { get; set; }
        public User? Employees { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        [StringLength(10)]
        public string? Status { get; set; } 
    }
}
