using HostelManagemantSystem.Data;
using HostelManagemantSystem.HostelServices.IServices;
using HostelManagemantSystem.Models;

namespace HostelManagemantSystem.HostelServices.Services
{
    public class AdminDasboardService  : IAdminDasboardServices 
    {
        private readonly HostelDbContext _hostelDbContext;
        public AdminDasboardService(HostelDbContext hostelDbContext)
        {
              _hostelDbContext = hostelDbContext;
        }
        public async Task<List<Beds>> GetAvaliablebedsAsync()
        {
            var Beds = await _hostelDbContext.Beds
                .Where(x => x.IsActive == false)
                .Include(x => x.Hostels)
                .ToListAsync();

        }
    }
}
