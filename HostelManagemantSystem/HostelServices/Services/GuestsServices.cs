using HostelManagemantSystem.Data;
using HostelManagemantSystem.DTO;
using HostelManagemantSystem.HostelServices.IServices;
using HostelManagemantSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HostelManagemantSystem.HostelServices.Services
{
    public class GuestsServices : IGuestsServices
    {
        private readonly HostelDbContext _hostelDbContext;
        private readonly IWebHostEnvironment _env;
        public GuestsServices(HostelDbContext hostelDbContext, IWebHostEnvironment env)
        {
            _hostelDbContext = hostelDbContext;
            _env = env;
        }
        public async Task<List<GuestsManagemant>> GetAllGuestsAsync(int hostelId)
        {
            var guests = await _hostelDbContext.Guests
                .Include(g => g.User)
                .Include(g => g.Beds)
                .ThenInclude(b => b.Rooms)
                .Where(g => g.HostelId == hostelId)
                .AsNoTracking()
                .ToListAsync();
            return guests.Select(g => new GuestsManagemant
            {
                GuestId = g.Id,
                FullName = g.User.FirstName + " " + g.User.LastName,
                Email = g.User.Email,
                phone = g.User.PhoneNumber,
                Address = g.User.Address ?? "N/A",
                RoomNumber = g.Beds?.Rooms?.RoomNumber.ToString() ?? "N/A",
                BedNumber = g.Beds?.BedNumber.ToString() ?? "N/A",
                NextPayment = g.NextPaymentDate.ToDateTime(TimeOnly.MinValue),
                status = g.IsActive
            }).ToList();
        }
        public async Task<bool> RegisterGuestsAsync(NewGuestsRegister dto)
        {
            using var transaction = await _hostelDbContext.Database.BeginTransactionAsync();

            try
            {
                var checkuser = await _hostelDbContext.Users
                     .FirstOrDefaultAsync(x => x.Email == dto.Email || x.PhoneNumber == dto.phonenumber);
                if (checkuser != null)
                {
                    throw new Exception("User is already Exists");
                }
                var user = new User
                {
                    FirstName = dto.Firstname,
                    LastName = dto.Lastname,
                    Email = dto.Email,
                    PhoneNumber = dto.phonenumber,
                    Address = dto.Address,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                    RoleId = 3,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true,
                    HostelId = dto.HostelId
                };

                _hostelDbContext.Users.Add(user);
                await _hostelDbContext.SaveChangesAsync();

                string? filePath = null;
                if (dto.IdProof != null)
                {
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var fileName = $"{Guid.NewGuid()}_{dto.IdProof.FileName}";
                    var fullPath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await dto.IdProof.CopyToAsync(stream);
                    }

                    filePath = "/uploads/" + fileName;
                }

                var Guests = new Guests
                {
                    UserId = user.ID,
                    HostelId = dto.HostelId,
                    BedId = dto.BedId,
                    CheckInDate = dto.CheckInDate,
                    NextPaymentDate = dto.CheckInDate.AddMonths(1),
                    SecurityDeposit = dto.SecurityDeposit,
                    MothlyRent = dto.MonthlyRent,
                    IdProof = filePath,
                    IsActive = true
                };
                _hostelDbContext.Guests.Add(Guests);

                var Beds = await _hostelDbContext.Beds
                    .Include(x => x.Rooms)
                    .FirstOrDefaultAsync(x => x.Id == dto.BedId);

                if (Beds != null) Beds.IsActive = true;

                await _hostelDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
        public async Task<bool> UpdateGuestsAsync(int guestId, UpdateGuest dto)
        {
            try
            {
                var guest = await _hostelDbContext.Guests
                    .Include(g => g.User)
                    .FirstOrDefaultAsync(g => g.Id == guestId);

                if (guest == null) return false;

                // Update guest fields
                guest.SecurityDeposit = dto.SecurityDeposit ?? guest.SecurityDeposit;
                guest.NextPaymentDate = dto.NextPaymentDate ?? guest.NextPaymentDate;
                guest.CheckInDate = dto.CheckInDate ?? guest.CheckInDate;
                //guest.MonthlyRent = dto.MonthlyRent ?? guest.MonthlyRent;

                // Update linked User fields
                if (guest.User != null)
                {
                    guest.User.FirstName = dto.FirstName ?? guest.User.FirstName;
                    guest.User.LastName = dto.LastName ?? guest.User.LastName;
                    guest.User.PhoneNumber = dto.Phone ?? guest.User.PhoneNumber;
                    guest.User.Address = dto.Address ?? guest.User.Address;
                }

                await _hostelDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> InActiveGuestsAsync(int guestId)
        {
            try
            {
                var guest = await _hostelDbContext.Guests.FindAsync(guestId);
                if (guest == null) return false;

                guest.IsActive = !guest.IsActive; // toggle active/inactive
                await _hostelDbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }

        }



    }
}
