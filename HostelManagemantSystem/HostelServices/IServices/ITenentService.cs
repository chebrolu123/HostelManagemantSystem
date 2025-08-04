using HostelManagemantSystem.DTO;

namespace HostelManagemantSystem.HostelServices.IServices
{
    public interface ITenentService
    {
        Task<TenentDashboard> GetDashboardAsync(int superAdminId);
    }
}
