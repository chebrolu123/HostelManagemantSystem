using System.ComponentModel.DataAnnotations;

namespace HostelManagemantSystem.Models
{
    public class Role
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public ICollection<User>? Users { get; set; } // Added property to fix CS1061
    }
}