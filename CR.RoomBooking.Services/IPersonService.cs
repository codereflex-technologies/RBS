using CR.RoomBooking.Data.Contexts;
using CR.RoomBooking.Services.Models;
using System;
using System.Threading.Tasks;

namespace CR.RoomBooking.Services
{
    public interface IPersonService
    {
        Task<PersonModel> GetAsync(int personId);
    }

    public class PersonService : IPersonService
    {
        RoomBookingsContext BookingsContext;

        public PersonService(RoomBookingsContext bookingsContext)
        {
            BookingsContext = bookingsContext; 
        }

        public async Task<PersonModel> GetAsync(int personId)
        {
             var p =  await BookingsContext.People.FindAsync(personId);
            throw new NotImplementedException();
        }
    }
}