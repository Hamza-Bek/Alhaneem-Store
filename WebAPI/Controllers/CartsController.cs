using Application.Dtos.Cart;
using Application.Interfaces;
using Application.Mappers;
using Application.Responses;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartsController : ControllerBase
{
   private readonly ICartRepository _cartRepository;

   public CartsController(ICartRepository cartRepository)
   {
      _cartRepository = cartRepository;
   }
   
   [HttpPost("cart")]
   public async Task<IActionResult> CreateCart([FromBody] string sessionId)
   {
      var response = await _cartRepository.CreateUserCartAsync(sessionId);
      
      return Ok(new ApiResponse<bool>(
         "Cart created successfully",
         true
      ));
   }

    [HttpGet("get/{sessionId}")]
    public async Task<IActionResult> GetCart([FromRoute] string sessionId)
    {
        var cart = await _cartRepository.GetUserCartByIdAsync(sessionId);

        if (cart is null)
            return NotFound(new ApiResponse<CartDto>("Cart not found", false));

        var cartDto = cart.ToDto();

        return Ok(new ApiResponse<CartDto>(
            "Cart retrieved successfully",
            true,
            cartDto
        ));
    }

    [HttpPost("item/update")]
    public async Task<IActionResult> UpdateItemQuantity([FromBody] UpdateCartItemRequest request)
    {
        try
        {
            var cart = await _cartRepository.UpdateItemQuantityAsync(request.ProductId, request.QuantityDelta, request.SessionId);
            return Ok(new ApiResponse<CartDto>("Cart updated successfully", true, cart.ToDto()));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<CartDto>(ex.Message, false));
        }
    }

    [HttpDelete("item")]
    public async Task<IActionResult> RemoveItem([FromQuery] Guid productId, [FromQuery] string sessionId)
    {
        try
        {
            var cart = await _cartRepository.RemoveItemCompletelyAsync(productId, sessionId);
            return Ok(new ApiResponse<CartDto>("Item removed successfully", true, cart.ToDto()));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<CartDto>(ex.Message, false));
        }
    }

    [HttpDelete("clear")]
   public async Task<IActionResult> ClearCart()
   {
      var response = await _cartRepository.ClearUserCartAsync();
      
      return Ok(new ApiResponse<bool>(
         "Cart cleared successfully",
         true,
         response
      ));
   }
   
}