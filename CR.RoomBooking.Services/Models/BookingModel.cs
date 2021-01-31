using CR.RoomBooking.Services.Models.Base;
using System;

namespace CR.RoomBooking.Services.Models
{
    public abstract class BookingModel : BaseModel
    {
        public int PersonId { get; set; }
        public int RoomId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string PersonFullName { get; set; }
        public string RoomName { get; set; }
    }
}
