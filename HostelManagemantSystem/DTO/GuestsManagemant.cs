using Microsoft.Identity.Client;

namespace HostelManagemantSystem.DTO
{
    public class GuestsManagemant
    {
        public int GuestId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? RoomNumber { get; set; }
        public string? BedNumber { get; set; }
        public string? phone { get; set; }
        public DateTime NextPayment { get; set; }
        public bool status { get; set; }
        public string? Address { get; set; }


    }
    public class NewGuestsRegister
    {
        //Users details
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? phonenumber { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }

        //Guests booking details
        public int HostelId { get; set; }
        public int RoomId { get; set; }
        public int BedId { get; set; }
        public int MonthlyRent { get; set; }
        public decimal SecurityDeposit { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateTime NextPaymentDate { get; set; }

        // File
        public IFormFile? IdProof { get; set; }


    }
    public class UpdateGuest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateOnly? NextPayment { get; set; }
        public decimal? SecurityDeposit { get; set; }
        public DateOnly? NextPaymentDate { get; set; }
        public DateOnly? CheckInDate { get; set; }


    }

}
