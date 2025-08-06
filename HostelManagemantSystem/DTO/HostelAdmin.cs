using System.ComponentModel.DataAnnotations;

namespace HostelManagemantSystem.DTO
{
    public class HostelAdmin
    {

        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
    public class AddHostelAdmin
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    }
    public class UpdateHostelAdmin
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
