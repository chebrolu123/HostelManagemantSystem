using HostelManagemantSystem.Data;
using HostelManagemantSystem.DTO;
using HostelManagemantSystem.HostelServices.IServices;
using HostelManagemantSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace HostelManagemantSystem.HostelServices.Services
{
    public class AdminService 
    {
        //private readonly HostelDbContext _hosteldbcontext;
        //public AdminService(HostelDbContext hostelDbContext)
        //{
        //    _hosteldbcontext = hostelDbContext;
        //}
        //public async Task<List<HostelAdmin>> GetAdminsForHostelAsync(int hostelId)
        //{
        //    var admins = await _hosteldbcontext.Users
        //        .Where(u => u.Hostels.Any(h => h.Id == hostelId) && u.Role.Name == "HostelAdmin")
        //        .ToListAsync();

        //    return admins("saved");
        //}
        //public async Task<User> AddHostelAdminAsync(int HostelId)
        //{

        //}
        //public async Task<User> UpdateHostelAdminAsync(int userId)
        //{

        //}
        //public async Task ToogleUserStatusAsync(int userId)
        //{

        //}
    }
}
