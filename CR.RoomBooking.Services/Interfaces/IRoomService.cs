using CR.RoomBooking.Services.Models;
using CR.RoomBooking.Services.Results;
using System;
using System.Threading.Tasks;

namespace CR.RoomBooking.Services.Interfaces
{
    public interface IRoomService
    {
        Task<ServiceResult> GetAsync(int id);
        Task<ServiceResult> GetAllAsync(string name);
        Task<ServiceResult> GetAvailableRoomsAsync(DateTime startDate, DateTime endDate);
        Task<ServiceResult> AddAsync(RoomRequestModel model);
        Task<ServiceResult> UpdateAsync(int id, RoomRequestModel model);
        Task<ServiceResult> RemoveAsync(int id, RemoveRoomModel model);
    }
}
