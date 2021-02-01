using CR.RoomBooking.Data.Contexts;
using CR.RoomBooking.Data.Domain;
using CR.RoomBooking.Data.Repositories;
using CR.RoomBooking.Services.Implementations;
using CR.RoomBooking.Services.Models;
using CR.RoomBooking.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CR.RoomBooking.Tests
{
    public class PeopleControllerTests
    {
        [Fact]
        public async Task GetAll_Person_Test()
        {
            await RunInContextAsync(async peopleController =>
            {
                var person1 = GetTestPersonModel();
                var person2 = GetTestPersonModel();

                //Act  
                await peopleController.AddAsync(person1);
                await peopleController.AddAsync(person2);

                var result = await peopleController.GetAllAsync(null, null, null, null, null);

                //Assert  
                Assert.Equal(2, ((IEnumerable<PersonModel>)result.Value).Count());
            });
        }

        [Fact]
        public async Task GetById_Person_Test()
        {
            await RunInContextAsync(async peopleController =>
            {
                var person = GetTestPersonModel();

                //Act  
                await peopleController.AddAsync(person);

                var personId = 1;
                var result = await peopleController.GetAsync(personId);

                //Assert  
                Assert.IsType<OkObjectResult>(result);
            });
        }

        [Fact]
        public async Task Add_Person_Test()
        {
            await RunInContextAsync(async peopleController =>
            {
                var person = GetTestPersonModel();

                //Act  
                var result = await peopleController.AddAsync(person);

                //Assert  
                Assert.IsType<OkObjectResult>(result);
            });
        }

        [Fact]
        public async Task Update_Person_Test()
        {
            await RunInContextAsync(async peopleController =>
            {
                var person = GetTestPersonModel();
                await peopleController.AddAsync(person);

                var personId = 1;
                person.FirstName = "Updated First Name";

                // Act
                var result = await peopleController.UpdateAsync(personId, person);

                //Assert  
                Assert.IsType<OkObjectResult>(result);
            });
        }

        [Fact]
        public async Task Remove_Person_Test()
        {
            await RunInContextAsync(async peopleController =>
            {
                var person = GetTestPersonModel();
                await peopleController.AddAsync(person);

                var personId = 1;

                // Act
                var result = await peopleController.RemoveAsync(personId);

                //Assert  
                Assert.IsType<OkObjectResult>(result);
            });
        }

        // Helpers

        private async Task RunInContextAsync(Func<PeopleController, Task> func)
        {
            var dbOptions = new DbContextOptionsBuilder<RoomBookingsContext>().UseInMemoryDatabase(databaseName: "RoomBookings" + DateTime.UtcNow.Millisecond)
                                                                              .Options;

            using (var context = new RoomBookingsContext(dbOptions))
            {
                var _mockPersonRepository = new Repository<Person>(context);
                var _mockPersonService = new PersonService(_mockPersonRepository);
                var _mockPeopleController = new PeopleController(_mockPersonService);

                await func(_mockPeopleController);
            }
        }

        private PersonModel GetTestPersonModel()
        {
            var person = new PersonModel()
            {
                FirstName = "Test First Name",
                LastName = "Test Last Name",
                PhoneNumber = "123",
                Email = "test@test.com",
                DateOfBirth = new DateTime(1996, 10, 10)
            };

            return person;
        }
    }
}
