using System.ComponentModel.DataAnnotations;

namespace HostelManagemantSystem.Models
{
    public class LoginAuth
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
