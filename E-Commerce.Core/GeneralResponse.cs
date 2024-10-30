namespace E_Commerce.Core
{
    public class GeneralResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public static GeneralResponse Success(object? data = null , string? message = "Operation Succeeded")
            => new GeneralResponse { IsSuccess = true, Data = data ,Message=message};
        public static GeneralResponse Failure(object? data = null, string errorMessage = "Operation failed")
            => new GeneralResponse {IsSuccess = false,Message = errorMessage,Data = data};
    }
}
    

