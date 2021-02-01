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
    public sealed class PersonService : IPersonService
    {
        private readonly IRepository<Person> _repository;

        public PersonService(IRepository<Person> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> AddAsync(PersonModel personModel)
        {
            try
            {
                if (personModel == null)
                {
                    throw new ArgumentNullException();
                }

                Person person = new Person(personModel.FirstName, personModel.LastName, personModel.PhoneNumber, personModel.Email, personModel.DateOfBirth);
                _repository.Add(person);
                await _repository.SaveChangesAsync();

                return ServiceResult.Success(person.Id);
            }
            catch (Exception e)
            {
                return ServiceResult.Error(e.Message);
            }
        }

        public async Task<ServiceResult> GetAllAsync(string firstName, string lastName, string email, string phoneNumber, DateTime? dateOfBirth)
        {
            try
            {
                var predicate = PredicateBuilder.New<Person>(true);

                if (!string.IsNullOrWhiteSpace(firstName))
                {
                    predicate = predicate.And(e => e.FirstName.StartsWith(firstName));
                }

                if (!string.IsNullOrWhiteSpace(lastName))
                {
                    predicate = predicate.And(e => e.LastName.StartsWith(lastName));
                }

                if (!string.IsNullOrWhiteSpace(email))
                {
                    predicate = predicate.And(e => e.Email.StartsWith(email));
                }

                if (!string.IsNullOrWhiteSpace(phoneNumber))
                {
                    predicate = predicate.And(e => e.PhoneNumber.StartsWith(phoneNumber));
                }

                if (dateOfBirth.HasValue)
                {
                    predicate = predicate.And(e => e.DateOfBirth == dateOfBirth);
                }

                var query = _repository.Table.Where(predicate);

                List<PersonModel> result = await query.AsNoTracking()
                                                      .Select(e => new PersonModel()
                                                      {
                                                          Id = e.Id,
                                                          FirstName = e.FirstName,
                                                          LastName = e.LastName,
                                                          Email = e.Email,
                                                          PhoneNumber = e.PhoneNumber,
                                                          DateOfBirth = e.DateOfBirth
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
                Person person = await _repository.Table.AsNoTracking()
                                                       .FirstOrDefaultAsync(e => e.Id == id);

                var result = person == null ? null
                                            : new PersonModel()
                                            {
                                                Id = person.Id,
                                                FirstName = person.FirstName,
                                                LastName = person.LastName,
                                                Email = person.Email,
                                                PhoneNumber = person.PhoneNumber,
                                                DateOfBirth = person.DateOfBirth
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
                Person person = await _repository.Table.AsNoTracking()
                                                       .FirstOrDefaultAsync(e => e.Id == id);

                if (person == null)
                {
                    return ServiceResult.Success(0);
                }

                _repository.Remove(person);
                await _repository.SaveChangesAsync();

                return ServiceResult.Success(person.Id);
            }
            catch (Exception e)
            {
                return ServiceResult.Error(e.Message);
            }
        }

        public async Task<ServiceResult> UpdateAsync(int id, PersonModel personModel)
        {
            try
            {
                Person person = await _repository.Table.AsNoTracking()
                                                       .FirstOrDefaultAsync(e => e.Id == id);

                if (person == null)
                {
                    return ServiceResult.Success(0);
                }

                var local = _repository.Context.Set<Person>().Local.FirstOrDefault(e => e.Id == id);

                if (local != null)
                {
                    _repository.Context.Entry(local).State = EntityState.Detached;
                }

                person.UpdateFields(personModel.FirstName, personModel.LastName, personModel.PhoneNumber, personModel.Email, person.DateOfBirth);
                _repository.Update(person);
                await _repository.SaveChangesAsync();

                return ServiceResult.Success(person.Id);
            }
            catch (Exception e)
            {
                return ServiceResult.Error(e.Message);
            }
        }
    }
}
