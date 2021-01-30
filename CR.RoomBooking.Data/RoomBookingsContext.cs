using Microsoft.EntityFrameworkCore;
using CR.RoomBooking.Data.Domain;

namespace CR.RoomBooking.Data
{
    public class RoomBookingsContext : DbContext
    {
        public RoomBookingsContext(DbContextOptions<RoomBookingsContext> options)
            : base(options)
        {
        }

        public DbSet<Person> People { get; set; }

        public DbSet<Room> Rooms { get; set; }
    }
}