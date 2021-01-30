using System.Collections.Generic;

namespace CR.RoomBooking.Data.Domain
{
    public class Room
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public ICollection<RoomBooking> Bookings { get; set; }
    }
}