using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCmodel.Models;

public sealed class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
{
    // Constructor for dependency injection
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public ApplicationDbContext() { }

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


}
