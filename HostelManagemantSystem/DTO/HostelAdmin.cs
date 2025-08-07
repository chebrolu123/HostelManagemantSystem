using System.ComponentModel.DataAnnotations;

namespace HostelManagemantSystem.DTO
{
    public class AddAdmin
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase and one number.")]
        public string Password { get; set; }
        public string PhoneNumber { get; set; } 
    }
    public class UpadteAdmin
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
    public class UpdateStatus
    {
        
        public bool IsActive { get; set; }
    }
    public class HostelDropDown
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public class HostelAdmin
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int HostelId { get; set; }
    }
}
