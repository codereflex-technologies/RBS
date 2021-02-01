using CR.RoomBooking.Services.Interfaces;
using CR.RoomBooking.Services.Models;
using CR.RoomBooking.Web.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CR.RoomBooking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : BaseController
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<ObjectResult> GetAllAsync([FromQuery] string name)
        {
            var result = await _roomService.GetAllAsync(name);

            return BaseResult(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ObjectResult> GetAsync([FromRoute] int id)
        {
            var result = await _roomService.GetAsync(id);

            return BaseResult(result);
        }

        [HttpPost]
        public async Task<ObjectResult> AddAsync([FromBody] RoomModel model)
        {
            var result = await _roomService.AddAsync(model);

            return BaseResult(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ObjectResult> UpdateAsync([FromRoute] int id, [FromBody] RoomModel model)
        {
            var result = await _roomService.UpdateAsync(id, model);

            return BaseResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ObjectResult> RemoveAsync([FromRoute] int id)
        {
            var result = await _roomService.RemoveAsync(id);

            return BaseResult(result);
        }
    }
}
