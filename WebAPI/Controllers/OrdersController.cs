using Application.Dtos.Order;
using Application.Interfaces;
using Application.Mappers;
using Application.Responses;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrdersController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpPost("submit")]
    public async Task<IActionResult> SubmitOrder([FromBody] UpdateOrderRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.SessionId))
            return BadRequest("SessionId is required.");

        var result = await _orderRepository.SubmitOrderAsync(request.SessionId);

        if (result.IsFailed)
        {
            return BadRequest(new ApiErrorResponse(
                errorMessage: result.Errors.First().Message,                
                errors : result.Errors.Select(e => e.Message).ToList()
            ));
        }

        return Ok(new ApiResponse(
            message: "Order made successfully",
            succeeded: true            
        ));
    }

    [HttpGet]
    public async Task<IActionResult> GetLastOrder(UpdateOrderRequest request)
    {
        var response = await _orderRepository.GetLastOrderAsync(request.SessionId);

        if (response is null)
        {
            return NotFound(new ApiResponse<OrderDto>(
                "No order found",
                false
            ));
        }

        var orderDto = response.ToDto();

        return Ok(new ApiResponse<OrderDto>(
            "orders retrieved successfully",
            true,
            orderDto
        ));
    }
}