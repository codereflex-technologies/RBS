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
    public class RoomsControllerTests
    {
        [Fact]
        public async Task GetAll_Rooms_Test()
        {
            await RunInContextAsync(async (roomsController, peopleController) =>
            {
                var roomsCount = 2;
                var room1 = GetTestRoomModel();
                var room2 = GetTestRoomModel();

                //Act  
                await roomsController.AddAsync(room1);
                await roomsController.AddAsync(room2);

                var result = await roomsController.GetAllAsync(null);

                //Assert  
                Assert.Equal(roomsCount, ((IEnumerable<RoomModel>)result.Value).Count());
            });
        }

        [Fact]
        public async Task Get_AvailableRooms_Test()
        {
            await RunInContextAsync(async (roomsController, peopleController) =>
            {
                var availableRoomsCount = 1;
                var room1Id = 1;
                var personId = 1;

                var room1 = GetTestRoomModel();
                var room2 = GetTestRoomModel();
                room2.Name = "Test room 2";
                var person = GetTestPersonModel();

                //Act  
                await roomsController.AddAsync(room1);
                await roomsController.AddAsync(room2);
                await peopleController.AddAsync(person);

                var date = DateTime.Now;
                BookingRequestModel booking1 = new BookingRequestModel()
                {
                    PersonId = personId,
                    RoomId = room1Id,
                    StartDate = date,
                    EndDate = date.AddMinutes(30)
                };

                BookingRequestModel booking2 = new BookingRequestModel()
                {
                    PersonId = personId,
                    RoomId = room1Id,
                    StartDate = date.AddMinutes(50),
                    EndDate = date.AddMinutes(80)
                };

                await roomsController.BookAsync(booking1);
                await roomsController.BookAsync(booking2);

                var result = await roomsController.GetAvailableRoomsAsync(date.AddMinutes(40), date.AddMinutes(80));

                //Assert  
                Assert.Equal(availableRoomsCount, ((IEnumerable<RoomModel>)result.Value).Count());
            });
        }

        [Fact]
        public async Task GetById_Room_Test()
        {
            await RunInContextAsync(async (roomsController, peopleController) =>
            {
                var roomId = 1;
                var room = GetTestRoomModel();

                //Act  
                await roomsController.AddAsync(room);

                var result = await roomsController.GetAsync(roomId);

                //Assert  
                Assert.IsType<OkObjectResult>(result);
            });
        }

        [Fact]
        public async Task Add_Room_Test()
        {
            await RunInContextAsync(async (roomsController, peopleController) =>
            {
                var room = GetTestRoomModel();

                //Act  
                var result = await roomsController.AddAsync(room);

                //Assert  
                Assert.IsType<OkObjectResult>(result);
            });
        }

        [Fact]
        public async Task Add_ValidRoomBooking_Test()
        {
            await RunInContextAsync(async (roomsController, peopleController) =>
            {
                var roomId = 1;
                var personId = 1;
                var room = GetTestRoomModel();
                var person = GetTestPersonModel();

                //Act  
                await roomsController.AddAsync(room);
                await peopleController.AddAsync(person);

                var date = DateTime.Now;
                BookingRequestModel booking = new BookingRequestModel()
                {
                    PersonId = personId,
                    RoomId = roomId,
                    StartDate = date,
                    EndDate = date.AddMinutes(30)
                };

                var result = await roomsController.BookAsync(booking);

                //Assert  
                Assert.IsType<OkObjectResult>(result);
            });
        }

        [Fact]
        public async Task Add_InvalidRoomBooking_Test()
        {
            await RunInContextAsync(async (roomsController, peopleController) =>
            {
                var roomId = 1;
                var personId = 1;
                var room = GetTestRoomModel();
                var person = GetTestPersonModel();

                //Act  
                await roomsController.AddAsync(room);
                await peopleController.AddAsync(person);

                var date = DateTime.Now;
                BookingRequestModel booking = new BookingRequestModel()
                {
                    PersonId = personId,
                    RoomId = roomId,
                    StartDate = date,
                    EndDate = date.AddHours(1.5)
                };

                var result = await roomsController.BookAsync(booking);

                //Assert  
                Assert.IsType<BadRequestObjectResult>(result);
            });
        }

        [Fact]
        public async Task Remove_RoomBooking_Test()
        {
            await RunInContextAsync(async (roomsController, peopleController) =>
            {
                var roomId = 1;
                var personId = 1;
                var bookingId = 1;
                var room = GetTestRoomModel();
                var person = GetTestPersonModel();

                //Act  
                await roomsController.AddAsync(room);
                await peopleController.AddAsync(person);

                var date = DateTime.Now;
                BookingRequestModel booking = new BookingRequestModel()
                {
                    PersonId = personId,
                    RoomId = roomId,
                    StartDate = date,
                    EndDate = date.AddMinutes(30)
                };

                await roomsController.BookAsync(booking);
                var result = await roomsController.RemoveBookingAsync(bookingId);

                //Assert  
                Assert.IsType<OkObjectResult>(result);
            });
        }

        [Fact]
        public async Task Update_Room_Test()
        {
            await RunInContextAsync(async (roomsController, peopleController) =>
            {
                var roomId = 1;
                var room = GetTestRoomModel();
                await roomsController.AddAsync(room);

                room.Name = "Updated Name";

                // Act
                var result = await roomsController.UpdateAsync(roomId, room);

                //Assert  
                Assert.IsType<OkObjectResult>(result);
            });
        }

        [Fact]
        public async Task Remove_Room_Test()
        {
            await RunInContextAsync(async (roomsController, peopleController) =>
            {
                var roomId = 1;
                var room = GetTestRoomModel();
                await roomsController.AddAsync(room);

                // Act
                var result = await roomsController.RemoveAsync(roomId, null);

                //Assert  
                Assert.IsType<OkObjectResult>(result);
            });
        }

        // Helpers

        private async Task RunInContextAsync(Func<RoomsController, PeopleController, Task> func)
        {
            var dbOptions = new DbContextOptionsBuilder<RoomBookingsContext>().UseInMemoryDatabase(databaseName: "RoomBookings" + new Random().Next(1, 100000))
                                                                              .Options;

            using (var context = new RoomBookingsContext(dbOptions))
            {
                var _mockBookingRepository = new Repository<Booking>(context);
                var _mockPersonRepository = new Repository<Person>(context);
                var _mockPersonService = new PersonService(_mockPersonRepository);
                var _mockPeopleController = new PeopleController(_mockPersonService);
                var _mockBookingService = new BookingService(_mockBookingRepository);

                var _mockRoomRepository = new Repository<Room>(context);
                var _mockRoomService = new RoomService(_mockRoomRepository, _mockBookingRepository);
                var _mockRoomsController = new RoomsController(_mockRoomService, _mockBookingService);

                await func(_mockRoomsController, _mockPeopleController);
            }
        }

        private RoomRequestModel GetTestRoomModel()
        {
            var room = new RoomRequestModel()
            {
                Name = "Test Room",
            };

            return room;
        }

        private PersonRequestModel GetTestPersonModel()
        {
            var person = new PersonRequestModel()
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
