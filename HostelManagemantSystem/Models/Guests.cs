using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagemantSystem.Models
{
    public class Guests
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Required]
        [ForeignKey("Hostels")]
        public int HostelId { get; set; }
        public Hostels? Hostels { get; set; }
        [Required]
        [ForeignKey("Beds")]
        public int BedId { get; set; }
        public Beds? Beds { get; set; }
        [Required]
        public DateOnly CheckInDate { get; set; }
        [Required]
        public DateOnly NextPaymentDate { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SecurityDeposit { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        public int MothlyRent { get; set; }

        public string? IdProof { get; set; }

        public ICollection<Payments>? Payments { get; set; }
    }
}
