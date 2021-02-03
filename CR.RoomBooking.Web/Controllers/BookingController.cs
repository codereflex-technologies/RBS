using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CR.RoomBooking.Services.Interfaces;
using CR.RoomBooking.Services.Models;
using CR.RoomBooking.Web.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CR.RoomBooking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : BaseController
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {

            _bookingService = bookingService;
        }
        [HttpPost("bookings")]
        public async Task<ObjectResult> BookAsync([FromBody] BookingRequestModel model)
        {
            var result = await _bookingService.BookAsync(model);

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
