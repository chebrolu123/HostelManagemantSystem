using HostelManagemantSystem.Data;
using HostelManagemantSystem.HostelServices.IServices;
using HostelManagemantSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HostelManagemantSystem.HostelServices.Services
{
    public class UsersService : IUserServices
    {
        private readonly HostelDbContext _context;

        public UsersService(HostelDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            var data = await _context.Users.ToListAsync();
            return data;
        }
        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }
        public async Task AddUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }
            user.CreatedAt = DateTime.Now;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User> LoginUserAsync(string email,string password)
        {
            var data = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
            return data;
        }
    }
}
