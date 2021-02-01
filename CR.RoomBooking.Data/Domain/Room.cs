﻿using CR.RoomBooking.Data.Domain.Base;
using System;
using System.Collections.Generic;

namespace CR.RoomBooking.Data.Domain
{
    public sealed class Room : BaseEntity
    {
        public string Name { get; private set; }

        public ICollection<Booking> Bookings { get; private set; }

        private Room() { }

        public Room(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Cannot create the entity");
            }

            Name = name;
        }

        public void UpdateFields(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Cannot update the entity");
            }

            Name = name;
        }
    }
}