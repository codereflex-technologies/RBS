using System;
using System.Threading.Tasks;
using CR.RoomBooking.Data;
using CR.RoomBooking.Services.Models;
using System.Linq;

namespace CR.RoomBooking.Services
{
    public interface IPersonService
    {
        Task<PersonInfo> GetAsync(int personId);
    }

    public class PersonService : IPersonService
    {
        RoomBookingsContext BookingsContext;
        public PersonService(RoomBookingsContext bookingsContext)
        {
            BookingsContext = bookingsContext; 
        }

        public async Task<PersonInfo> GetAsync(int personId)
        {
             var p =  await BookingsContext.People.FindAsync(personId);
            throw new NotImplementedException();
        }
    }
}