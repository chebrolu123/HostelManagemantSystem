
using System.ComponentModel.DataAnnotations;

namespace HostelManagemantSystem.DTO
{
    public class TenentDashboard
    {
        public int TotalHostels { get; set; }
        public int TotalGuests { get; set; }
        public decimal MonthlyProfitLoss { get; set; }
        public List<HostelOverView>? Hostels { get; set; }


    }
    public class HostelOverView
    { 
        public int HostelId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        

        public bool IsActive { get; set; } = true;

    }
    public class CreateHostel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public string? LicenseExpiryDate { get; set; }
    }
    public class UpdateHostel
    {
       
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public string? LicenseExpiryDate { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
