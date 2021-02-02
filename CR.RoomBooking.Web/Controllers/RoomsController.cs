using CR.RoomBooking.Services.Interfaces;
using CR.RoomBooking.Services.Models;
using CR.RoomBooking.Web.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CR.RoomBooking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : BaseController
    {
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;

        public RoomsController(IRoomService roomService, IBookingService bookingService)
        {
            _roomService = roomService;
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ObjectResult> GetAllAsync([FromQuery] string name)
        {
            var result = await _roomService.GetAllAsync(name);

            return BaseResult(result);
        }

        [HttpGet("available")]
        public async Task<ObjectResult> GetAvailableRoomsAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _roomService.GetAvailableRoomsAsync(startDate, endDate);

            return BaseResult(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ObjectResult> GetAsync([FromRoute] int id)
        {
            var result = await _roomService.GetAsync(id);

            return BaseResult(result);
        }

        [HttpPost]
        public async Task<ObjectResult> AddAsync([FromBody] RoomRequestModel model)
        {
            var result = await _roomService.AddAsync(model);

            return BaseResult(result);
        }

        [HttpPost("bookings")]
        public async Task<ObjectResult> BookAsync([FromBody] BookingRequestModel model)
        {
            var result = await _bookingService.BookAsync(model);

            return BaseResult(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ObjectResult> UpdateAsync([FromRoute] int id, [FromBody] RoomRequestModel model)
        {
            var result = await _roomService.UpdateAsync(id, model);

            return BaseResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ObjectResult> RemoveAsync([FromRoute] int id, [FromBody] RemoveRoomModel model)
        {
            var result = await _roomService.RemoveAsync(id, model);

            return BaseResult(result);
        }

        [HttpDelete("bookings/{id:int}")]
        public async Task<ObjectResult> RemoveBookingAsync([FromRoute] int id)
        {
            var result = await _bookingService.RemoveAsync(id);

            return BaseResult(result);
        }
    }
}
