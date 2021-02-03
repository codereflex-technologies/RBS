using System.Net;

namespace CR.RoomBooking.Services.Results
{
    public sealed class ServiceResult
    {
        public bool IsSucceeded { get; private set; }
        public object Data { get; private set; }
        public string ErrorMessage { get; private set; }
        public HttpStatusCode StatusCode { get; set; }

        public static ServiceResult Success(object data)
        {
            return new ServiceResult() { IsSucceeded = true, Data = data , StatusCode = HttpStatusCode.OK };
        }

        public static ServiceResult Error(string errorMessage, HttpStatusCode status)
        {
            return new ServiceResult() { IsSucceeded = false, ErrorMessage = errorMessage , StatusCode = status };
        }
    }
}
