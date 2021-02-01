using CR.RoomBooking.Data.Domain;
using CR.RoomBooking.Data.Repositories;
using CR.RoomBooking.Services.Interfaces;
using CR.RoomBooking.Services.Models;
using CR.RoomBooking.Services.Results;
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
        private readonly IRepository<Room> _repository;

        public RoomService(IRepository<Room> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> AddAsync(RoomModel roomModel)
        {
            try
            {
                if (roomModel == null)
                {
                    throw new ArgumentNullException();
                }

                Room room = new Room(roomModel.Name);
                _repository.Add(room);
                await _repository.SaveChangesAsync();

                return ServiceResult.Success(room.Id);
            }
            catch (Exception e)
            {
                return ServiceResult.Error(e.Message);
            }
        }

        public async Task<ServiceResult> GetAllAsync(string name)
        {
            try
            {
                var predicate = PredicateBuilder.New<Room>(true);

                if (!string.IsNullOrWhiteSpace(name))
                {
                    predicate = predicate.And(e => e.Name.StartsWith(name));
                }

                var query = _repository.Table.Where(predicate);

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

        public async Task<ServiceResult> GetAsync(int id)
        {
            try
            {
                Room room = await _repository.Table.AsNoTracking()
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

        public async Task<ServiceResult> RemoveAsync(int id)
        {
            try
            {
                Room room = await _repository.Table.AsNoTracking()
                                                   .FirstOrDefaultAsync(e => e.Id == id);

                if (room == null)
                {
                    return ServiceResult.Success(0);
                }

                _repository.Remove(room);
                await _repository.SaveChangesAsync();

                return ServiceResult.Success(room.Id);
            }
            catch (Exception e)
            {
                return ServiceResult.Error(e.Message);
            }
        }

        public async Task<ServiceResult> UpdateAsync(int id, RoomModel roomModel)
        {
            try
            {
                Room room = await _repository.Table.AsNoTracking()
                                                   .FirstOrDefaultAsync(e => e.Id == id);

                if (room == null)
                {
                    return ServiceResult.Success(0);
                }

                var local = _repository.Context.Set<Room>().Local.FirstOrDefault(e => e.Id == id);

                if (local != null)
                {
                    _repository.Context.Entry(local).State = EntityState.Detached;
                }
                
                room.UpdateFields(roomModel.Name);
                _repository.Update(room);
                await _repository.SaveChangesAsync();

                return ServiceResult.Success(room.Id);
            }
            catch (Exception e)
            {
                return ServiceResult.Error(e.Message);
            }
        }
    }
}
