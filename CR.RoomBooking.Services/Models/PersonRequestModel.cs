﻿using System;

namespace CR.RoomBooking.Services.Models
{
    public sealed class PersonRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
