using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagemantSystem.Models
{
    public class Expenditure
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Hostels")]
        public int HostelId { get; set; }
        public Hostels? Hostels { get; set; }
        [Required]
        [StringLength(50)]
        public string? Category { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Required]
        [StringLength(255)]
        public string? Description { get; set; }
        [Required]
        public DateOnly ExpenseDate { get; set; }


    }
}
