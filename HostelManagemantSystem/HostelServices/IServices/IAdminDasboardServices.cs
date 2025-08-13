using HostelManagemantSystem.Models;

namespace HostelManagemantSystem.HostelServices.IServices
{
    public interface IAdminDasboardServices
    {
        Task<List<Beds>> GetAvaliablebedsAsync();

    }
}
