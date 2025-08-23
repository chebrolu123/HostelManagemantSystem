using HostelManagemantSystem.DTO;

namespace HostelManagemantSystem.HostelServices.IServices
{
    public interface IGuestsServices
    {
        Task<List<GuestsManagemant>> GetAllGuestsAsync(int hostelId);
        Task<bool> RegisterGuestsAsync(NewGuestsRegister dto);       
        Task<bool> UpdateGuestsAsync(int guestId, UpdateGuest dto);
        Task<bool> InActiveGuestsAsync(int guestId);
    }
}
