using HostelManagemantSystem.Data;
using HostelManagemantSystem.DTO;
using HostelManagemantSystem.HostelServices.IServices;
using HostelManagemantSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HostelManagemantSystem.HostelServices.Services
{
    public class TenentService : ITenentService
    {
        public readonly HostelDbContext _hostelDbContext;
        public TenentService(HostelDbContext hostelDbContext)
        {
            _hostelDbContext = hostelDbContext;
        }

        public async Task<TenentDashboard> GetDashboardAsync(int superAdminId)
        {
            // Step 1: Find all hostel IDs managed by this Super Admin.
            var managedHostelIds = await _hostelDbContext.Hostels
                .Where(h => h.SuperAdminId == superAdminId)
                .Select(h => h.Id)
                .ToListAsync();

            // If the admin manages no hostels, return an empty dashboard.
            if (!managedHostelIds.Any())
            {
                return new TenentDashboard(); // Returns a DTO with 0s and empty lists
            }

            // Step 2: Get the overview details for only those hostels.
            var hostelOverviews = await _hostelDbContext.Hostels
                .Where(h => managedHostelIds.Contains(h.Id)) // Filter by the list of IDs
                .Select(h => new HostelOverView
                {
                    HostelId = h.Id,
                    Name = h.Name,
                    Address = h.Address,
                    LicenseExpiryDate = h.LicenseExpiryDate,
                    IsActive = h.IsActive
                })
                .ToListAsync();

            // Step 3: Calculate metrics using the list of hostel IDs.
            int totalHostels = hostelOverviews.Count;

            // Count guests that belong to any of the managed hostels.
            int totalGuests = await _hostelDbContext.Guests
                .CountAsync(g => managedHostelIds.Contains(g.HostelId)); // Assumes Guest has HostelId

            DateTime now = DateTime.UtcNow;

            // Sum payments from any of the managed hostels for the current month.
            decimal monthlyProfitLoss = await _hostelDbContext.Payments
                .Where(p => managedHostelIds.Contains(p.Id) && // Assumes Payment has HostelId
                            p.PaymentDate.Month == now.Month &&
                            p.PaymentDate.Year == now.Year)
                .SumAsync(p => p.Amount);

            // Step 4: Return the complete DTO.
            return new TenentDashboard
            {
                TotalHostels = totalHostels,
                TotalGuests = totalGuests,
                MonthlyProfitLoss = monthlyProfitLoss,
                Hostels = hostelOverviews 
            };
        }
        public async Task<Hostels> CreateHostelAsync(CreateHostel create, int SuperAdminId)
        {
            var Hostel = new Hostels
            {
                Name = create.Name,
                Address = create.Address,
                LicenseExpiryDate = DateTime.Parse(create.LicenseExpiryDate), 
                IsActive = true, 
                //CreatedAt = DateTime.UtcNow,
                
                SuperAdminId = SuperAdminId
            };

            _hostelDbContext.Hostels.Add(Hostel);
            await _hostelDbContext.SaveChangesAsync();
            return Hostel;

        }
        public async Task<Hostels> UpdateHostelAsync(int Id, UpdateHostel update, int SuperAdminId)
        {
            var UpadteHostels = await _hostelDbContext.Hostels
                .FirstOrDefaultAsync(h => h.Id == Id && h.SuperAdminId == SuperAdminId);
            if (UpadteHostels == null)
            {
                return null;
            }
            UpadteHostels.Name = update.Name;
            UpadteHostels.Address = update.Address;
            UpadteHostels.LicenseExpiryDate = DateTime.Parse(update.LicenseExpiryDate);
            UpadteHostels.IsActive = update.IsActive;
            _hostelDbContext.Hostels.Update(UpadteHostels);
            await _hostelDbContext.SaveChangesAsync();
            return UpadteHostels;
        }



    }
}


