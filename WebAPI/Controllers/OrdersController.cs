using Application.Interfaces;
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

    [HttpPost]
    public async Task<IActionResult> SubmitOrder(string sessionId)
    {
        var response = await _orderRepository.SubmitOrderAsync(sessionId);

        return Ok(new ApiResponse<IEnumerable<Order>>(
            "order made successfully",
            true
        ));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetLastOrder(string sessionId)
    {
        var response = await _orderRepository.GetLastOrderAsync(sessionId);

        return Ok(new ApiResponse<Order>(
            "orders retrieved successfully",
            true,
            response
        ));
    }
}