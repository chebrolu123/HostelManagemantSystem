using Microsoft.AspNetCore.Connections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagemantSystem.Models
{
    public class Beds
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Rooms")]
        public int RoomId { get; set; }
        public Rooms? Rooms { get; set; }
        [Required]
        [StringLength(10)]
        public string? BedNumber { get; set; }
        [Required]
        public decimal CostPerMonth { get; set; }
        public ICollection<Guests>? Guests { get; set; }

        public bool IsActive { get; set; } = true;

    }
}
