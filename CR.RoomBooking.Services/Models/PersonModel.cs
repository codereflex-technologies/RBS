using CR.RoomBooking.Services.Models.Base;
using System;

namespace CR.RoomBooking.Services.Models
{
    public sealed class PersonModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}