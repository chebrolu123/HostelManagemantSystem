using HostelManagemantSystem.DTO;
using HostelManagemantSystem.Models;

namespace HostelManagemantSystem.HostelServices.IServices
{
    public interface IAdminservices
    {
        Task<IEnumerable<HostelDropDown>> GetHostelNamesAsync();

        Task<IEnumerable<HostelAdmin>> GetAdminsByHostelIdAsync(int Id);
        Task<User?> AddAdminAsync( int HostelId ,AddAdmin addAdmin);
        Task<User?> UpdateAdminAsync(int UserId, UpadteAdmin updateAdmin);
       
       Task<bool> UpdateStautsAsync(int UserId, bool IsActive);

        Task<bool> IsEmailExistsAsync(string email);

    }
}
