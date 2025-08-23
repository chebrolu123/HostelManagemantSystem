using HostelManagemantSystem.Data;
using HostelManagemantSystem.DTO;
using HostelManagemantSystem.HostelServices.IServices;
using HostelManagemantSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HostelManagemantSystem.HostelServices.Services
{
    public class AdminDasboardService : IAdminDasboardServices
    {
        private readonly HostelDbContext _hostelDbContext;
        public AdminDasboardService(HostelDbContext hostelDbContext)
        {
              _hostelDbContext = hostelDbContext;
        }
        public async Task<AdminDashboard> GetAdminDashboardAsync(int hostelId)
        {
            var CurrentYear = DateTime.Now.Year;

            var AvailableBeds = await _hostelDbContext.Beds 
                .CountAsync(b => b.Rooms.HostelId == hostelId && !b.IsActive);

            var guestsWithPendingPayments = await _hostelDbContext.Payments
                .CountAsync(p => p.Guest.HostelId == hostelId && !p.IsPaid);

            var monthlyExpenditures = await _hostelDbContext.Expenditures
                .Where(e => e.HostelId == hostelId && e.ExpenseDate.Year == CurrentYear)
                .GroupBy(e => new { e.ExpenseDate.Month, e.Category })
                .Select(g => new MonthlyExpenditureDto
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(g.Key.Month),
                    Category = g.Key.Category,
                    Amount = g.Sum(x => x.Amount)
                })
                .ToListAsync();

            var monthlyIncome = await _hostelDbContext.Payments
                .Where(p => p.Guest.HostelId == hostelId && p.IsPaid && p.PaymentDate.Year == CurrentYear)
                .GroupBy(p => p.PaymentDate.Month)
                .Select(g => new { Month = g.Key, Income = g.Sum(x => x.Amount) })
                .ToListAsync();

            var monthlyExpenses = await _hostelDbContext.Expenditures
                .Where(e => e.HostelId == hostelId && e.ExpenseDate.Year == CurrentYear)
                .GroupBy(e => e.ExpenseDate.Month)
                .Select(g => new { Month = g.Key, Expense = g.Sum(x => x.Amount) })
                .ToListAsync();

            var yearlyProfit = monthlyIncome
                .Select(i => new YearlyProfitDto
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(i.Month),
                    Profit = i.Income - (monthlyExpenses.FirstOrDefault(e => e.Month == i.Month)?.Expense ?? 0)
                })
                .ToList();

            var recentPayments = await _hostelDbContext.Payments
                .Where(p => p.Guest.HostelId == hostelId && p.IsPaid)
                .OrderByDescending(p => p.PaymentDate)
                .Take(5)
                .Select(p => new RecentPaymentDto
                {
                    PaymentId = p.Id,
                    Amount = p.Amount,
                    Date = p.PaymentDate
                })
                .ToListAsync();

            var roomWise = await _hostelDbContext.Rooms
                .Where(r => r.HostelId == hostelId)
                .Select(r => new RoomWiseDto
                {
                    RoomNumber = r.RoomNumber,
                    VacantBeds = r.Beds.Count(b => !b.IsActive),
                    OccupiedBeds = r.Beds.Count(b => b.IsActive)
                })
                .ToListAsync();

            return new AdminDashboard
            {
                AvailableBeds = AvailableBeds, // Corrected variable name casing
                GuestsWithPendingPayments = guestsWithPendingPayments,
                MonthlyExpenditures = monthlyExpenditures,
                YearlyProfit = yearlyProfit,
                RecentPayments = recentPayments,
                RoomWise = roomWise
            };
        }



    }
}
