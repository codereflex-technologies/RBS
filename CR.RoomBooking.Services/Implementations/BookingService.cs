using CR.RoomBooking.Data.Domain;
using CR.RoomBooking.Data.Repositories;
using CR.RoomBooking.Services.Interfaces;
using CR.RoomBooking.Services.Models;
using CR.RoomBooking.Services.Results;
using CR.RoomBooking.Utilities.Error;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CR.RoomBooking.Services.Implementations
{
    public sealed class BookingService : IBookingService
    {
        private readonly IRepository<Booking> _repository;

        public BookingService(IRepository<Booking> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> BookAsync(BookingRequestModel model)
        {
            try
            {
                if (model.StartDate > model.EndDate)
                {
                    return ServiceResult.Error(ErrorMessages.InvalidDates);
                }

                if ((model.EndDate - model.StartDate).TotalHours > 1)
                {
                    return ServiceResult.Error(ErrorMessages.TimeRangeLimit);
                }

                Booking booking = new Booking(model.PersonId, model.RoomId, model.StartDate, model.EndDate);
                _repository.Add(booking);
                await _repository.SaveChangesAsync();

                return ServiceResult.Success(booking.Id);
            }
            catch (Exception e)
            {
                return ServiceResult.Error(e.Message);
            }
        }

        public async Task<ServiceResult> RemoveAsync(int id)
        {
            try
            {
                Booking booking = await _repository.Table.AsNoTracking()
                                                         .FirstOrDefaultAsync(e => e.Id == id);

                if (booking == null)
                {
                    return ServiceResult.Error(ErrorMessages.NotFound);
                }

                _repository.Remove(booking);
                await _repository.SaveChangesAsync();

                return ServiceResult.Success(booking.Id);
            }
            catch (Exception e)
            {
                return ServiceResult.Error(e.Message);
            }
        }
    }
}
