using E_Commerce.Repository.Data.Contexts;
using E_Commerce_API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : BaseController
    {
        private readonly StoreDbContext context;

        public ErrorsController(StoreDbContext context)
        {
            this.context = context;
        }
        // a specific route to test the not found error
        [Route("/not-found")]
        [HttpGet]
        public IActionResult NotFoundError()
        {
            return NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound));
        }
        [HttpGet("server-error")]
        public IActionResult ServerError()
        {
            var brand = context.Brands.Find(42);
            var brandName = brand.Name;
            return Ok();
        }
        [HttpGet("bad-request")]
        public IActionResult BadRequestError()
        {
            return BadRequest(new ApiErrorsResponse(StatusCodes.Status400BadRequest));
        }
        [HttpGet("unauthorized")]
        public IActionResult UnauthorizedError()
        {
            return Unauthorized(new ApiErrorsResponse(StatusCodes.Status401Unauthorized));
        }
        // validation Error like sending a string instead of an integer
        [HttpGet("bad-request/{id}")]
        public IActionResult ValidationError(int id)
        {
            return BadRequest(new ApiValidationErrorResponse());
        }
    }
}
