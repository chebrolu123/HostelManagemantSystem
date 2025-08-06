using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagemantSystem.Models
{
    public class Hostels
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        
        [Required]
        [StringLength(200)]
        public string? Address { get; set; }

        [ForeignKey("User")]
        public int SuperAdminId { get; set; }
        public User? User { get; set; }
        [Required]
        public DateTime LicenseExpiryDate { get; set; }
        [Required]
        public  bool IsActive { get; set; } = true;

        public ICollection<Rooms>? Rooms { get; set; }
        public ICollection<Guests>? Guests { get; set; }
        public ICollection<Employees>? Employees { get; set; }
        public ICollection<Expenditure>? Expenditure { get; set; }
    }
}
