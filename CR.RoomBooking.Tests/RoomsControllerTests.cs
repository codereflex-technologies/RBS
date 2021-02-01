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
            await RunInContextAsync(async roomsController =>
            {
                var room1 = GetTestRoomModel();
                var room2 = GetTestRoomModel();

                //Act  
                await roomsController.AddAsync(room1);
                await roomsController.AddAsync(room2);

                var result = await roomsController.GetAllAsync(null);

                //Assert  
                Assert.Equal(2, ((IEnumerable<RoomModel>)result.Value).Count());
            });
        }

        [Fact]
        public async Task GetById_Room_Test()
        {
            await RunInContextAsync(async roomsController =>
            {
                var room = GetTestRoomModel();

                //Act  
                await roomsController.AddAsync(room);

                var roomId = 1;
                var result = await roomsController.GetAsync(roomId);

                //Assert  
                Assert.IsType<OkObjectResult>(result);
            });
        }

        [Fact]
        public async Task Add_Room_Test()
        {
            await RunInContextAsync(async roomsController =>
            {
                var room = GetTestRoomModel();

                //Act  
                var result = await roomsController.AddAsync(room);

                //Assert  
                Assert.IsType<OkObjectResult>(result);
            });
        }

        [Fact]
        public async Task Update_Room_Test()
        {
            await RunInContextAsync(async roomsController =>
            {
                var room = GetTestRoomModel();
                await roomsController.AddAsync(room);

                var roomId = 1;
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
            await RunInContextAsync(async roomsController =>
            {
                var room = GetTestRoomModel();
                await roomsController.AddAsync(room);

                var roomId = 1;

                // Act
                var result = await roomsController.RemoveAsync(roomId);

                //Assert  
                Assert.IsType<OkObjectResult>(result);
            });
        }

        // Helpers

        private async Task RunInContextAsync(Func<RoomsController, Task> func)
        {
            var dbOptions = new DbContextOptionsBuilder<RoomBookingsContext>().UseInMemoryDatabase(databaseName: "RoomBookings" + DateTime.UtcNow.Millisecond)
                                                                              .Options;

            using (var context = new RoomBookingsContext(dbOptions))
            {
                var _mockRoomRepository = new Repository<Room>(context);
                var _mockRoomService = new RoomService(_mockRoomRepository);
                var _mockRoomsController = new RoomsController(_mockRoomService);

                await func(_mockRoomsController);
            }
        }

        private RoomModel GetTestRoomModel()
        {
            var room = new RoomModel()
            {
                Name = "Test Room",
            };

            return room;
        }
    }
}
