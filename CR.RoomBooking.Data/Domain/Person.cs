using System;

namespace CR.RoomBooking.Data.Domain
{
    public class Person
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime DateOfBirth { get; set; }
    }
}