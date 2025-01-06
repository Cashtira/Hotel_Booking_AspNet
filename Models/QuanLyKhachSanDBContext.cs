using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;

namespace QuanLyKhachSan.Models
{
    public class QuanLyKhachSanDBContext : DbContext
    {
        public QuanLyKhachSanDBContext() : base("DBConnectionString")
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles{ get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<BookingService> BookingServices { get; set; }

        public DbSet<Type> Types { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<RoomComment> RoomComments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + ": " + x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }
        }

    }
}