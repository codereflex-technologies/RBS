using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CR.RoomBooking.Services;
using CR.RoomBooking.Services.Models;

namespace CR.RoomBooking.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("{personId}")]
        public async Task<ActionResult<PersonInfo>> Get(int personId)
        {
            var person = await _personService.GetAsync(personId);
            return person;
        }
    }
}
