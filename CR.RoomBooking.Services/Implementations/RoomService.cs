using CR.RoomBooking.Data.Domain;
using CR.RoomBooking.Data.Repositories;
using CR.RoomBooking.Services.Interfaces;
using CR.RoomBooking.Services.Models;
using CR.RoomBooking.Services.Results;
using CR.RoomBooking.Utilities.Error;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CR.RoomBooking.Services.Implementations
{
    public sealed class RoomService : IRoomService
    {
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<Booking> _bookingRepository;

        public RoomService(IRepository<Room> roomRepository, IRepository<Booking> bookingRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
        }

        /// <summary>
        /// Add a room
        /// </summary>
        public async Task<ServiceResult> AddAsync(RoomRequestModel model)
        {
            try
            {
                if (model == null || string.IsNullOrWhiteSpace(model.Name))
                {
                    return ServiceResult.Error(ErrorMessages.InvalidModel);
                }

                Room room = new Room(model.Name);
                _roomRepository.Add(room);
                await _roomRepository.SaveChangesAsync();

                return ServiceResult.Success(room.Id);
            }
            catch (Exception e)
            {
                return ServiceResult.Error(e.Message);
            }
        }

        /// <summary>
        ///  Get all with the given filtering parameters
        /// </summary>
        public async Task<ServiceResult> GetAllAsync(string name)
        {
            try
            {
                // Creating predicate for filtering entities
                var predicate = PredicateBuilder.New<Room>(true);

                if (!string.IsNullOrWhiteSpace(name))
                {
                    predicate = predicate.And(e => e.Name.StartsWith(name));
                }

                var query = _roomRepository.Table.Where(predicate);

                List<RoomModel> result = await query.AsNoTracking()
                                                    .Select(e => new RoomModel()
                                                    {
                                                        Id = e.Id,
                                                        Name = e.Name
                                                    })
                                                    .ToListAsync();

                return ServiceResult.Success(result);
            }
            catch (Exception e)
            {
                return ServiceResult.Error(e.Message);
            }
        }

        /// <summary>
        ///  Get available rooms in the given datetime range
        /// </summary>
        public async Task<ServiceResult> GetAvailableRoomsAsync(DateTime startDate, DateTime endDate)
        {
            List<RoomModel> result = await _roomRepository.Table.Include(e => e.Bookings)
                                                          .AsNoTracking()
                                                          .Where(e => e.Bookings.All(b => (startDate > b.EndDate && startDate < b.StartDate // Case 1: When the given range is between to bookings
                                                                                          && endDate > b.EndDate && endDate < b.StartDate)
                                                                                          || (endDate > b.EndDate && startDate > b.EndDate ) // Case 2: When the given range is next the bookings
                                                                                          || (endDate < b.StartDate && startDate < b.StartDate))) // Case 3: When the given range is before the bookings
                                                          .Select(e => new RoomModel()
                                                          {
                                                              Id = e.Id,
                                                              Name = e.Name
                                                          })
                                                          .ToListAsync();

            return ServiceResult.Success(result);
        }

        /// <summary>
        /// Get by id
        /// </summary>
        public async Task<ServiceResult> GetAsync(int id)
        {
            try
            {
                Room room = await _roomRepository.Table.AsNoTracking()
                                                       .FirstOrDefaultAsync(e => e.Id == id);

                var result = room == null ? null
                                          : new RoomModel()
                                          {
                                              Id = room.Id,
                                              Name = room.Name
                                          };

                return ServiceResult.Success(result);
            }
            catch (Exception e)
            {
                return ServiceResult.Error(e.Message);
            }
        }

        /// <summary>
        /// Remove the room
        /// </summary>
        public async Task<ServiceResult> RemoveAsync(int id, RemoveRoomModel model)
        {
            try
            {
                Room room = await _roomRepository.Table.Include(e => e.Bookings)
                                                       .AsNoTracking()
                                                       .FirstOrDefaultAsync(e => e.Id == id);

                if (room == null)
                {
                    return ServiceResult.Error(ErrorMessages.NotFound);
                }

                // If we are looking for transferring the bookings to another room
                if (model != null && model.MoveBookings)
                {
                    foreach (var booking in room.Bookings.Select(b => new Booking(b.PersonId, model.NewRoomId, b.StartDate, b.EndDate)))
                    {
                        _bookingRepository.Add(booking);
                    }
                }

                _roomRepository.Remove(room);
                await _roomRepository.SaveChangesAsync();

                return ServiceResult.Success(room.Id);
            }
            catch (Exception e)
            {
                return ServiceResult.Error(e.Message);
            }
        }

        /// <summary>
        /// Update the room
        /// </summary>
        public async Task<ServiceResult> UpdateAsync(int id, RoomRequestModel model)
        {
            try
            {
                Room room = await _roomRepository.Table.AsNoTracking()
                                                       .FirstOrDefaultAsync(e => e.Id == id);

                if (room == null)
                {
                    return ServiceResult.Error(ErrorMessages.NotFound);
                }

                var local = _roomRepository.Context.Set<Room>().Local.FirstOrDefault(e => e.Id == id);

                if (local != null)
                {
                    _roomRepository.Context.Entry(local).State = EntityState.Detached;
                }

                room.UpdateFields(model.Name);
                _roomRepository.Update(room);
                await _roomRepository.SaveChangesAsync();

                return ServiceResult.Success(room.Id);
            }
            catch (Exception e)
            {
                return ServiceResult.Error(e.Message);
            }
        }
    }
}
