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
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate leads to career change",
                _ => null
            };
        }
    }
}
