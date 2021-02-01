using CR.RoomBooking.Services.Models;
using CR.RoomBooking.Services.Results;
using System.Threading.Tasks;

namespace CR.RoomBooking.Services.Interfaces
{
    public interface IRoomService
    {
        Task<ServiceResult> GetAsync(int id);
        Task<ServiceResult> GetAllAsync(string name);
        Task<ServiceResult> AddAsync(RoomModel roomModel);
        Task<ServiceResult> UpdateAsync(int id, RoomModel roomModel);
        Task<ServiceResult> RemoveAsync(int id);
    }
}
