using AutoMapper;
using E_Commerce.Core;
using E_Commerce.Core.DTOs;
using E_Commerce.Core.Models;
using E_Commerce.Repository.Data.Repos;
using E_Commerce_API.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Authorize]
    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basket;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository basket,IMapper mapper)
        {
            _basket = basket;
            this.mapper = mapper;
        }
        [ProducesResponseType<CustomerBasket>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorsResponse>(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> GetBasket(string basketId)
        {
            var basket = await _basket.GetBasketAsync(basketId);
            return Ok(basket != null ? GeneralResponse.Success(basket) : new CustomerBasket { Id = basketId });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBasket(CustomerBasketDTO basketDTO)
        {
            var updated = await _basket.UpdateBasketAsync(basketDTO);
            return Ok(updated);
        }
        [ProducesResponseType<CustomerBasket>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorsResponse>(StatusCodes.Status404NotFound)]
        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string basketId)
        {
            var deleted = await _basket.DeleteBasketAsync(basketId);
            return deleted ? Ok(GeneralResponse.Success(message:"Basket deleted successfully"))
                : NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound, "Basket not found"));
        }
    }
}
