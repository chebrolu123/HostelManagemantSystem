using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagemantSystem.Models
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Hostels")]
        public int HostelId { get; set; }
        public Hostels? Hostels { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        [Required]
        [StringLength(50)]
        public string? Role { get; set; }
        [Required]
        public DateOnly JoinDate { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal MonthlySalary { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;

        public ICollection<EmployeeAttendence>? EmployeeAttendences { get; set; }
    }
}
