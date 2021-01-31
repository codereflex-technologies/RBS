using CR.RoomBooking.Data.Domain.Base;
using System;
using System.Collections.Generic;

namespace CR.RoomBooking.Data.Domain
{
    public sealed class Person : BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public DateTime DateOfBirth { get; private set; }

        public ICollection<Booking> Bookings { get; private set; }

        private Person() { }

        public Person(string firstName,
                      string lastName,
                      string phoneNumber,
                      string email,
                      DateTime dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(firstName)
                || string.IsNullOrWhiteSpace(lastName)
                || string.IsNullOrWhiteSpace(PhoneNumber))
            {
                throw new Exception("Cannot create the entity");
            }

            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            DateOfBirth = dateOfBirth;
        }
    }
}