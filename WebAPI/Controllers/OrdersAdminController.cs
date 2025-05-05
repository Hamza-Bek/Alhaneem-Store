using Application.Dtos.Order;
using Application.Interfaces;
using Application.Mappers;
using Application.Responses;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller;

[ApiController]
[Route("api/[controller]")]
public class OrdersAdminController : ControllerBase
{
    private readonly IOrderAdminRepository _orderAdminRepository;

    public OrdersAdminController(IOrderAdminRepository orderAdminRepository)
    {
        _orderAdminRepository = orderAdminRepository;
    }

    [HttpGet("orders")]
    public async Task<IActionResult> GetAllOrdersAsync()
    {
        var response = await _orderAdminRepository.GetAllOrdersAsync();
        return Ok(new ApiResponse<List<OrderDto>>(
            "Orders retrieved successfully",
            true,
            response.Select(o => o.ToDto()).ToList()
        ));
    }

    [HttpGet("order/{id}")]
    public async Task<IActionResult> GetOrderByIdAsync(Guid id)
    {
        var response = await _orderAdminRepository.GetOrderByIdAsync(id);
        if (response == null)
            return NotFound(new ApiResponse<OrderDto>(
                "Order not found",
                false
            ));
        return Ok(new ApiResponse<OrderDto>(
            "Order retrieved successfully",
            true,
            response.ToDto()
        ));
    }

    [HttpPut("order/update")]
    public async Task<IActionResult> UpdateOrderStatusAsync(Guid orderId, OrderStatus status)
    {
        var response = await _orderAdminRepository.UpdateOrderStatusAsync(orderId, status);
        if (!response)
            return NotFound(new ApiResponse<bool>(
                "Order not found",
                false
            ));
        return Ok(new ApiResponse<bool>(
            "Order status updated successfully",
            true,
            response
        ));
    }

    [HttpPut("order/delivery/update")]
    public async Task<IActionResult> UpdateDeliveryStatusAsync(Guid orderId, DeliveryStatus status)
    {
        var response = await _orderAdminRepository.UpdateDeliveryStatusAsync(orderId, status);
        if (!response)
            return NotFound(new ApiResponse<bool>(
                "Order not found",
                false
            ));
        return Ok(new ApiResponse<bool>(
            "Order delivery status updated successfully",
            true,
            response
        ));
    }
}
