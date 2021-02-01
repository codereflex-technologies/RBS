using CR.RoomBooking.Services.Results;
using Microsoft.AspNetCore.Mvc;

namespace CR.RoomBooking.Web.Controllers.Base
{
    public abstract class BaseController : ControllerBase
    {
        [NonAction]
        public ObjectResult BaseResult(ServiceResult result)
        {
            return result.IsSucceeded ? (ObjectResult)Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
    }
}
