using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagemantSystem.Models
{
    public class Payments
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Guest")]
        public int GuestId { get; set; }
        public Guests? Guest { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public int ForMonth { get; set; }
        [Required]
        public int ForYear { get; set; }
        [Required]
        [StringLength(50)]
        public string? PaymentMode { get; set; }
        public bool IsPaid { get; set; } = false;
    }
}
