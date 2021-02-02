using CR.RoomBooking.Services.Models;
using CR.RoomBooking.Services.Results;
using System.Threading.Tasks;

namespace CR.RoomBooking.Services.Interfaces
{
    public interface IBookingService
    {
        Task<ServiceResult> BookAsync(BookingRequestModel model);
        Task<ServiceResult> RemoveAsync(int id);
    }
}
