using HostelManagemantSystem.DTO;
using HostelManagemantSystem.Models;

namespace HostelManagemantSystem.HostelServices.IServices
{
    public interface IAdminservices
    {
        Task<List<HostelAdmin>> GetAdminsForHostelAsync(int hostelId);
        Task AddHostelAdminAsync(int hostelId);
        Task UpdateHostelAdminAsync(int userId);
        Task ToggleUserStatusAsync(int userId);

    }
}
