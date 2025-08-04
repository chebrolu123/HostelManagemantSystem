using HostelManagemantSystem.Data;
using HostelManagemantSystem.DTO;
using HostelManagemantSystem.HostelServices.IServices;
using Microsoft.EntityFrameworkCore;

namespace HostelManagemantSystem.HostelServices.Services
{
    public class TenentService 
    {
        public readonly HostelDbContext _hostelDbContext;
        public TenentService(HostelDbContext hostelDbContext)
        {
            _hostelDbContext = hostelDbContext;
        }

        public async Task<TenentDashboard> GetDashboardAsync(int superAdminId)
        {
            var hostels = await _hostelDbContext.Hostels
                .Where(h => h.SuperAdminId == superAdminId)
                .ToListAsync();

            var hostelIds = hostels.Select(h => h.Id).ToList();

            var guestCount = await _hostelDbContext.Guests
                .Where(g => hostelIds.Contains(g.HostelId) && g.IsActive)
                .CountAsync();

            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var monthlyRevenue = await (from p in _hostelDbContext.Payments
                                        join g in _hostelDbContext.Guests on p.GuestId equals g.Id
                                        where p.ForMonth == currentMonth
                                           && p.ForYear == currentYear
                                           && hostelIds.Contains(g.HostelId)
                                        select p.Amount)
                                       .SumAsync();

            return new TenentDashboard
            {
                TotalHostels = hostels.Count,
                TotalGuests = guestCount,
                MonthlyProfitLoss = monthlyRevenue, 
                TotalHostels = hostels.Select(h => new HostelOverView
                {
                    Name = h.Name,
                    Address = h.Address,
                    LicenseExpiryDate = h.LicenseExpiryDate,
                    IsActive = h.IsActive
                }).ToList()
            };
        }

    }
}

