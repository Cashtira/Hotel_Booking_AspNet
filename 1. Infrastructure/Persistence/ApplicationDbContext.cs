namespace _1._Infrastructure.Persistence;

using _2._Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public sealed class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options) { }

    public DbSet<Booking> Bookings { get; set; } = null!;

    public DbSet<Feedback> Feedbacks { get; set; } = null!;

    public DbSet<Hotel> Hotels { get; set; } = null!;

    public DbSet<Housekeeping> Housekeepings { get; set; } = null!;

    public DbSet<Invoice> Invoices { get; set; } = null!;

    public DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; } = null!;

    public DbSet<Maintenance> Maintenances { get; set; } = null!;

    public DbSet<Room> Rooms { get; set; } = null!;

    public DbSet<RoomBooking> RoomBookings { get; set; } = null!;

    public DbSet<RoomType> RoomTypes { get; set; } = null!;

    public DbSet<Service> Services { get; set; } = null!;

    public DbSet<ServiceBooking> ServiceBookings { get; set; } = null!;

    public DbSet<UserBooking> UserBookings { get; set; } = null!;

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-COFIP2B;Initial Catalog=HotelTest;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;Command Timeout=300");*/
}
