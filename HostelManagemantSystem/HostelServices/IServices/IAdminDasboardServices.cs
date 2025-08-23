using HostelManagemantSystem.DTO;
using HostelManagemantSystem.Models;

namespace HostelManagemantSystem.HostelServices.IServices
{
    public interface IAdminDasboardServices
    {
        public  Task<AdminDashboard> GetAdminDashboardAsync(int hostelId);


    }
}
