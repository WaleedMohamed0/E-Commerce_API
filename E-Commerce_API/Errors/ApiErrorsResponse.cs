namespace E_Commerce_API.Errors
{
    public class ApiErrorsResponse
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public ApiErrorsResponse(int status, string? message = null)
        {
            Status = status;
            Message = message ?? GetDefaultMessageError(status);
        }
        private string? GetDefaultMessageError(int status)
        {
            return status switch
            {
                400 => "The request was invalid or cannot be processed.",
                401 => "Access denied. Please provide valid credentials.",
                404 => "The requested resource could not be found.",
                500 => "An internal server error occurred. Please try again later.",
                _ => null
            };
        }
    }
}
