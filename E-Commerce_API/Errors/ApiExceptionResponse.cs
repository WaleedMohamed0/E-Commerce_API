namespace E_Commerce_API.Errors
{
    public class ApiExceptionResponse : ApiErrorsResponse
    {
        public string? Details { get; set; }
        public ApiExceptionResponse(int status, string? message = null, string? details = null) : base(status, message)
        {
            Details = details;
        }
    }
}
