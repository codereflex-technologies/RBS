using CR.RoomBooking.Services.Interfaces;
using CR.RoomBooking.Services.Models;
using CR.RoomBooking.Web.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CR.RoomBooking.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : BaseController
    {
        private readonly IPersonService _personService;

        public PeopleController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<ObjectResult> GetAllAsync([FromQuery] string firstName,
                                                                [FromQuery] string lastName,
                                                                [FromQuery] string email,
                                                                [FromQuery] string phoneNumber,
                                                                [FromQuery] DateTime? dateOfBirth)
        {
            var result = await _personService.GetAllAsync(firstName, lastName, email, phoneNumber, dateOfBirth);

            return BaseResult(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ObjectResult> GetAsync([FromRoute] int id)
        {
            var result = await _personService.GetAsync(id);

            return BaseResult(result);
        }

        [HttpPost]
        public async Task<ObjectResult> AddAsync([FromBody] PersonModel model)
        {
            var result = await _personService.AddAsync(model);

            return BaseResult(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ObjectResult> UpdateAsync([FromRoute] int id, [FromBody] PersonModel model)
        {
            var result = await _personService.UpdateAsync(id, model);

            return BaseResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ObjectResult> RemoveAsync([FromRoute] int id)
        {
            var result = await _personService.RemoveAsync(id);

            return BaseResult(result);
        }
    }
}
