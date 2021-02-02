using CR.RoomBooking.Services.Models;
using CR.RoomBooking.Services.Results;
using System;
using System.Threading.Tasks;

namespace CR.RoomBooking.Services.Interfaces
{
    public interface IPersonService
    {
        /// <summary>
        /// Get by id
        /// </summary>
        Task<ServiceResult> GetAsync(int id);

        /// <summary>
        ///  Get all with the given filtering parameters
        /// </summary>
        Task<ServiceResult> GetAllAsync(string firstName, string lastName, string email, string phoneNumber, DateTime? dateOfBirth);

        /// <summary>
        /// Add a person
        /// </summary>
        Task<ServiceResult> AddAsync(PersonRequestModel model);

        /// <summary>
        /// Update the person
        /// </summary>
        Task<ServiceResult> UpdateAsync(int id, PersonRequestModel model);

        /// <summary>
        /// Remove the person
        /// </summary>
        Task<ServiceResult> RemoveAsync(int id);
    }
}