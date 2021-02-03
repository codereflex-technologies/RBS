using CR.RoomBooking.Data.Contexts;
using CR.RoomBooking.Data.Domain;
using CR.RoomBooking.Data.Repositories;
using CR.RoomBooking.Services.Implementations;
using CR.RoomBooking.Services.Models;
using CR.RoomBooking.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CR.RoomBooking.Tests
{
    public class BookingsControllerTests
    {
        [Fact]
        public async Task Add_RoomBooking_Test()
        {
            await RunInContextAsync(async (roomsController, peopleController, bookingsController) =>
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

                var result = await bookingsController.BookAsync(booking);

                //Assert  
                Assert.IsType<OkObjectResult>(result);
            });
        }

        [Fact]
        public async Task Add_RoomBooking_TimeRange_Max__Test()
        {
            await RunInContextAsync(async (roomsController, peopleController, bookingsController) =>
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

                var result = await bookingsController.BookAsync(booking);

                //Assert  
                Assert.IsType<BadRequestObjectResult>(result);
            });
        }

        [Fact]
        public async Task Remove_RoomBooking_Test()
        {
            await RunInContextAsync(async (roomsController, peopleController, bookingsController) =>
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

                await bookingsController.BookAsync(booking);
                var result = await bookingsController.RemoveBookingAsync(bookingId);

                //Assert  
                Assert.IsType<OkObjectResult>(result);
            });
        }

        [Fact]
        public async Task Remove_RoomBooking_Not_Found_Test()
        {
            await RunInContextAsync(async (roomsController, peopleController, bookingsController) =>
            {
                var roomId = 1;
                var personId = 1;
                var bookingId = 2;
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

                await bookingsController.BookAsync(booking);
                var result = await bookingsController.RemoveBookingAsync(bookingId);

                //Assert  
                Assert.IsType<NotFoundObjectResult>(result);
            });
        }

        // Helpers

        private async Task RunInContextAsync(Func<RoomsController, PeopleController, BookingsController, Task> func)
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
                var _mockRoomsController = new RoomsController(_mockRoomService);

                var _mockBookingsController = new BookingsController(_mockBookingService);

                await func(_mockRoomsController, _mockPeopleController, _mockBookingsController);
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
