using CR.RoomBooking.Services.Models;
using CR.RoomBooking.Services.Results;
using System.Threading.Tasks;

namespace CR.RoomBooking.Services.Interfaces
{
    public interface IBookingService
    {
        /// <summary>
        /// Add a booking
        /// </summary>
        Task<ServiceResult> BookAsync(BookingRequestModel model);

        /// <summary>
        /// Remove the booking
        /// </summary>
        Task<ServiceResult> RemoveAsync(int id);
    }
}
