using CR.RoomBooking.Services.Results;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CR.RoomBooking.Web.Controllers.Base
{
    public abstract class BaseController : ControllerBase
    {
        [NonAction]
        public ObjectResult BaseResult(ServiceResult result)
        {
            ObjectResult serviceresult = default;
            if (result.IsSucceeded)
            {
                serviceresult = (ObjectResult)Ok(result.Data);
            }
            else
            {
                if (result.StatusCode == HttpStatusCode.NotFound)
                    serviceresult = NotFound(result.ErrorMessage);
                else if (result.StatusCode == HttpStatusCode.BadRequest)
                    serviceresult = BadRequest(result.ErrorMessage);
                else if (result.StatusCode == HttpStatusCode.InternalServerError)
                    serviceresult = BadRequest(HttpStatusCode.InternalServerError);
            }

            return serviceresult;
        }
    }
}
