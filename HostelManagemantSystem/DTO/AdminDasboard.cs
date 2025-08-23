namespace HostelManagemantSystem.DTO
{

    public class AdminDashboard
    {
        public int AvailableBeds { get; set; }
        public int GuestsWithPendingPayments { get; set; }
        public List<MonthlyExpenditureDto> MonthlyExpenditures { get; set; }
        public List<YearlyProfitDto> YearlyProfit { get; set; }
        public List<RecentPaymentDto> RecentPayments { get; set; }
        public List<RoomWiseDto> RoomWise { get; set; }   // extra: room-wise breakdown
    }

    public class MonthlyExpenditureDto
    {
        public string Month { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
    }

    public class YearlyProfitDto
    {
        public string Month { get; set; }
        public decimal Profit { get; set; }
    }

    public class RecentPaymentDto
    {
        public int PaymentId { get; set; }
        public string GuestName { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }

    public class RoomWiseDto
    {
        public string RoomNumber { get; set; }
        public int VacantBeds { get; set; }
        public int OccupiedBeds { get; set; }
    }



}
