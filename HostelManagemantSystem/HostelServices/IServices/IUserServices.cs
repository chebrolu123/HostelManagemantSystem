using HostelManagemantSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HostelManagemantSystem.HostelServices.IServices
{
    public interface IUserServices
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);

        Task<User> LoginUserAsync(string email, string password);
    }
}
