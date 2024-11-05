using AutoMapper;
using E_Commerce.Core.DTOs.Orders;
using E_Commerce.Core.Models.Order;
using E_Commerce.Repository.Data.Repos;
using E_Commerce.Service.Services.Orders;
using E_Commerce_API.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce_API.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public OrdersController(IOrderService orderService,IMapper mapper,IUnitOfWork unitOfWork)
        {
            this.orderService = orderService;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        [ProducesResponseType<OrderReturnDTO>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorsResponse>(StatusCodes.Status400BadRequest)]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDTO orderDTO)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized(new ApiErrorsResponse(StatusCodes.Status401Unauthorized));
            }
            var orderResponse = await orderService.CreateOrderAsync(userEmail, orderDTO.DeliveryMethodId,orderDTO.BasketId, orderDTO.ShipToAddress);
            if (orderResponse == null)
            {
                return BadRequest(new ApiErrorsResponse(StatusCodes.Status400BadRequest));
            }
            var orderReturnDTO = mapper.Map<OrderReturnDTO>(orderResponse);
            return Ok(orderReturnDTO);
        }
        [ProducesResponseType<IEnumerable<OrderReturnDTO>>(StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized(new ApiErrorsResponse(StatusCodes.Status401Unauthorized));
            }
            var orders = await orderService.GetOrdersForUserAsync(userEmail);
            if (orders == null)
            {
                return BadRequest(new ApiErrorsResponse(StatusCodes.Status400BadRequest));
            }
            var ordersReturnDTO = mapper.Map<IEnumerable<OrderReturnDTO>>(orders);
            return Ok(ordersReturnDTO);
        }

        [ProducesResponseType<OrderReturnDTO>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorsResponse>(StatusCodes.Status404NotFound)]
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized(new ApiErrorsResponse(StatusCodes.Status401Unauthorized));
            }
            var order = await orderService.GetOrderByIdAsync(id, userEmail);
            if (order == null)
            {
                return NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound));
            }
            var orderReturnDTO = mapper.Map<OrderReturnDTO>(order);
            return Ok(orderReturnDTO);
        }
        [ProducesResponseType<IEnumerable<DeliveryMethod>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorsResponse>(StatusCodes.Status404NotFound)]
        [HttpGet("DeliveryMethods")]
        public async Task<IActionResult> GetDeliveryMethods()
        {
            var deliveryMethods = await unitOfWork.genericRepository<DeliveryMethod, int>().GetAllAsync();
            if (deliveryMethods == null)
            {
                return NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound));
            }
            return Ok(deliveryMethods);
        }
    }
}
