using HostelManagemantSystem.DTO;
using HostelManagemantSystem.Models;

namespace HostelManagemantSystem.HostelServices.IServices
{
    public interface ITenentService
    {
        Task<TenentDashboard> GetDashboardAsync(int superAdminId);
        Task<Hostels> CreateHostelAsync(CreateHostel create,int SuperAdmin);
        Task<Hostels> UpdateHostelAsync(int Id, UpdateHostel update, int SuperAdminId);
        Task<Hostels> StatusUpadateAsync(int id, bool IsActive);
    }
}
