using System;

namespace CR.RoomBooking.Services.Models
{
    public sealed class BookingRequestModel
    {
        public int PersonId { get; set; }
        public int RoomId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
