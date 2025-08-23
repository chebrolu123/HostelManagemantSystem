using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace HostelManagemantSystem.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        [StringLength(15)]
        public string? PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string? PasswordHash { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public  Role? Role { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public int? HostelId { get; set; }

        [ForeignKey("HostelId")]
        public Hostels? Hostel { get; set; }
        public string? Address { get; set; }
        public ICollection<Hostels>? Hostels { get; set; }
        public ICollection<Guests>? Guests { get; set; }
       public ICollection<EmployeeAttendence>? EmployeeAttendences { get; set; }
       
    }
}
