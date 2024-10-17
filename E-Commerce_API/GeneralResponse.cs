using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API
{
    public class GeneralResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; } = null;

        public static GeneralResponse Success(object? data = null, string message = "Operation successful")
        {
            return new GeneralResponse
            {
                IsSuccess = true,
                Message = message,
                Data = data
            };
        }
        public static GeneralResponse Failure(string message = "Operation failed", object? data = null)
        {
            return new GeneralResponse
            {
                IsSuccess = false,
                Message = message,
                Data = data
            };
        }
    }
}
