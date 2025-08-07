using HostelManagemantSystem.Data;
using HostelManagemantSystem.DTO;
using HostelManagemantSystem.HostelServices.IServices;
using HostelManagemantSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;


namespace HostelManagemantSystem.HostelServices.Services
{
    public class AdminService : IAdminservices
    {
        private readonly HostelDbContext _HDbcontext;
        public AdminService(HostelDbContext context)
        {
            _HDbcontext = context;
        }
        public async Task<IEnumerable<HostelDropDown>> GetHostelNamesAsync()
        {
            var Dropdown = await _HDbcontext.Hostels
                .Select(h => new HostelDropDown
                {
                    Id = h.Id,
                    Name = h.Name
                }).ToListAsync();
            return Dropdown;
        }
        public async Task<IEnumerable<HostelAdmin>> GetAdminsByHostelIdAsync(int HostelId)
        {
            var admins = await _HDbcontext.Users
                    .Where(u => u.HostelId == HostelId && u.Role != null && u.Role.Name == "HostelAdmin")
                    .Select(u => new HostelAdmin
                    {
                        Id = u.ID,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        RoleId = u.RoleId, 
                        IsActive = u.IsActive,
                        CreatedAt = u.CreatedAt
                    }).ToListAsync();
            return admins;
        }
        public async Task<User> AddAdminAsync(int HostelId , AddAdmin createAdmin)
        {

            var Admin = await _HDbcontext.Hostels.FindAsync(HostelId);
            if (Admin == null)
            {
                return null;
            }
            var existingEmail = await _HDbcontext.Users.FirstOrDefaultAsync(u => u.Email == createAdmin.Email);
            if (existingEmail != null) return null;
            var user = new User
            {
                FirstName = createAdmin.FirstName,
                LastName = createAdmin.LastName,
                Email = createAdmin.Email,
                PhoneNumber = createAdmin.PhoneNumber,
                PasswordHash = createAdmin.Password, // Ensure password is hashed in the controller or service layer
                RoleId = 2, // Assuming 2 is the RoleId for HostelAdmin
                HostelId = HostelId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            _HDbcontext.Users.Add(user);
            await _HDbcontext.SaveChangesAsync();
            return user;

        }
        public async Task<User?> UpdateAdminAsync(int UserId , UpadteAdmin updateadmin)
        {
            var user = await _HDbcontext.Users.FindAsync(UserId);
            if (user == null)
            {
                return null;
            }
            user.FirstName = updateadmin.FirstName;
            user.LastName = updateadmin.LastName;
            user.Email = updateadmin.Email;
            _HDbcontext.Users.Update(user);
            await _HDbcontext.SaveChangesAsync();
            return user;
        }
        public async Task<bool> UpdateStautsAsync(int UserId, bool IsActive)
        {
            var user = await _HDbcontext.Users.FindAsync(UserId);
            if (user == null)
            {
                return false;
            }
            user.IsActive = IsActive;
            _HDbcontext.Users.Update(user);
            await _HDbcontext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _HDbcontext.Users.AnyAsync(u => u.Email == email);
        }

    }
}
