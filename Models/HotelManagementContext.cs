using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCmodel.Models;

public class HotelManagementContext : IdentityDbContext<AppUserModel>
{
    // Constructor for dependency injection
    public HotelManagementContext(DbContextOptions<HotelManagementContext> options) : base(options)
    {
    }

    // Parameterless constructor for design-time tools (like migrations)
    public HotelManagementContext() { }

    // DbSets for your entities
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Housekeeping> Housekeepings { get; set; }
    public DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }

    // This is where we configure the connection string (only in OnConfiguring if options not set externally)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-UG4BEPP;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }

    // Fluent API and entity configurations can go here
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships and entity settings with Fluent API here
    }
}
