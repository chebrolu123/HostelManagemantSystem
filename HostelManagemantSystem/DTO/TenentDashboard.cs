namespace HostelManagemantSystem.DTO
{
    public class TenentDashboard
    {
        public int TotalHostels { get; set; }
        public int TotalGuests { get; set; }
        public decimal MonthlyProfitLoss { get; set; }

    }
    public class HostelOverView
    { 
        public string? Name { get; set; }
        public string? Address { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        

        public bool IsActive { get; set; } = true;

    }

}
