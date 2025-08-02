using HostelManagemantSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HostelManagemantSystem.Data
{
    public class HostelDbContext : DbContext
    {
        public HostelDbContext(DbContextOptions<HostelDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Hostels> Hostels { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Beds> Beds { get; set; }
        public DbSet<Guests> Guests { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Expenditure> Expenditures { get; set; }
        public DbSet<EmployeeAttendence> EmployeeAttendences { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            //Users foreignKey to Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            //Hostels foreignKey to User
            modelBuilder.Entity<Hostels>()
                .HasOne(h => h.User)
                .WithMany(x => x.Hostels)
                .HasForeignKey(h => h.SuperAdminId)
                .OnDelete(DeleteBehavior.NoAction);

            //Rooms foreignKey to Hostels
            modelBuilder.Entity<Rooms>()
                .HasOne(r => r.Hostels)
                .WithMany(y => y.Rooms)
                .HasForeignKey(r => r.HostelId)
                .OnDelete(DeleteBehavior.NoAction);
            //Beds foreignKey to Rooms
            modelBuilder.Entity<Beds>()
                .HasOne(x => x.Rooms)
                .WithMany(x => x.Beds)
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.NoAction);
            //Guests ForeignKey to Users
            modelBuilder.Entity<Guests>()
                .HasOne(x => x.User)
                .WithMany(x => x.Guests)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            //Guests ForeignKey to Hostels
            modelBuilder.Entity<Guests>()
                .HasOne(x => x.Hostels)
                .WithMany(y => y.Guests)
                .HasForeignKey(x => x.HostelId)
                .OnDelete(DeleteBehavior.NoAction);
            //Guests ForeignKey to Beds
            modelBuilder.Entity<Guests>()
                .HasOne(x => x.Beds)
                .WithMany(y => y.Guests)
                .HasForeignKey(x => x.BedId)
                .OnDelete(DeleteBehavior.NoAction);
            //Payments ForeignKey to Guests
            modelBuilder.Entity<Payments>()
                .HasOne(p => p.Guest)
                .WithMany(g => g.Payments)
                .HasForeignKey(p => p.GuestId)
                .OnDelete(DeleteBehavior.NoAction);
            //Employees ForeignKey to Hostels
            modelBuilder.Entity<Employees>()
                .HasOne(x => x.Hostels)
                .WithMany(y => y.Employees)
                .HasForeignKey(x => x.HostelId)
                .OnDelete(DeleteBehavior.NoAction);
            ////EmployeeAttendance ForeignKey to Employees
            modelBuilder.Entity<EmployeeAttendence>()
                .HasOne(x => x.Employees)
                .WithMany(y => y.EmployeeAttendences)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);
            //Expenditures ForeignKey to Hostels
            modelBuilder.Entity<Expenditure>()
                .HasOne(x => x.Hostels)
                .WithMany(y => y.Expenditure)
                .HasForeignKey(x => x.HostelId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Beds>()
                .Property(x => x.CostPerMonth)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Hostels>()
                .Property(x => x.IsActive)
                .HasDefaultValue(true);
        }
    }
}
