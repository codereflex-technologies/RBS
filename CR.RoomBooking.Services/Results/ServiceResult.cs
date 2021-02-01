namespace CR.RoomBooking.Services.Results
{
    public sealed class ServiceResult
    {
        public bool IsSucceeded { get; private set; }
        public object Data { get; private set; }
        public string ErrorMessage { get; private set; }

        public static ServiceResult Success(object data)
        {
            return new ServiceResult() { IsSucceeded = true, Data = data };
        }

        public static ServiceResult Error(string errorMessage)
        {
            return new ServiceResult() { IsSucceeded = false, ErrorMessage = errorMessage };
        }
    }
}
