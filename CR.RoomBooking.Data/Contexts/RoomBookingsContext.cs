using CR.RoomBooking.Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace CR.RoomBooking.Data.Contexts
{
    public class RoomBookingsContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public RoomBookingsContext(DbContextOptions<RoomBookingsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoomBookingsContext).Assembly);
        }

    }
}