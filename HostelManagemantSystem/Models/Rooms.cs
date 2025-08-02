using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelManagemantSystem.Models
{
    public class Rooms
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Hostels")]
        public int HostelId { get; set; }
        public Hostels? Hostels { get; set; }

        [Required]
        public string? RoomNumber { get; set; }
        [Required]
        public int TotalBeds { get; set; }


        public ICollection<Beds>? Beds { get; set; }

    }

}

