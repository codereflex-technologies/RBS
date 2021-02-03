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
            ObjectResult apiResult = default;

            if (result.IsSucceeded)
            {
                apiResult = Ok(result.Data);
            }
            else
            {
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    apiResult = NotFound(result.ErrorMessage);
                }
                else if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    apiResult = BadRequest(result.ErrorMessage);
                }
                else if (result.StatusCode == HttpStatusCode.UnprocessableEntity)
                {
                    apiResult = UnprocessableEntity(result.ErrorMessage);
                }
                else if (result.StatusCode == HttpStatusCode.InternalServerError)
                {
                    apiResult = BadRequest(HttpStatusCode.InternalServerError);
                }
            }

            return apiResult;
        }
    }
}
