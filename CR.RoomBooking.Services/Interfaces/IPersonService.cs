using CR.RoomBooking.Services.Models;
using CR.RoomBooking.Services.Results;
using System;
using System.Threading.Tasks;

namespace CR.RoomBooking.Services.Interfaces
{
    public interface IPersonService
    {
        Task<ServiceResult> GetAsync(int id);
        Task<ServiceResult> GetAllAsync(string firstName, string lastName, string email, string phoneNumber, DateTime? dateOfBirth);
        Task<ServiceResult> AddAsync(PersonModel personModel);
        Task<ServiceResult> UpdateAsync(int id, PersonModel personModel);
        Task<ServiceResult> RemoveAsync(int id);
    }
}