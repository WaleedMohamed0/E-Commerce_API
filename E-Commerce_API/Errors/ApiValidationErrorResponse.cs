namespace E_Commerce_API.Errors
{
    public class ApiValidationErrorResponse : ApiErrorsResponse
    {
        public IEnumerable<string>? Errors { get; set; }
        public ApiValidationErrorResponse() : base(400)
        {
        }
    }
}
