using CR.RoomBooking.Services.Interfaces;
using CR.RoomBooking.Services.Models;
using CR.RoomBooking.Web.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CR.RoomBooking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : BaseController
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {

            _bookingService = bookingService;
        }
        [HttpPost]
        public async Task<ObjectResult> BookAsync([FromBody] BookingRequestModel model)
        {
            var result = await _bookingService.BookAsync(model);

            return BaseResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ObjectResult> RemoveBookingAsync([FromRoute] int id)
        {
            var result = await _bookingService.RemoveAsync(id);

            return BaseResult(result);
        }

    }
}
