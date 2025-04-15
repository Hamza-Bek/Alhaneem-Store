using Application.Dtos.Cart;
using Application.Interfaces;
using Application.Mappers;
using Application.Responses;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartsController : Controller
{
   private readonly ICartRepository _cartRepository;

   public CartsController(ICartRepository cartRepository)
   {
      _cartRepository = cartRepository;
   }
   
   [HttpPost("create")]
   public async Task<IActionResult> CreateCart([FromBody] string sessionId)
   {
      var response = await _cartRepository.CreateUserCartAsync(sessionId);
      
      return Ok(new ApiResponse<bool>(
         "Cart created successfully",
         true
      ));
   }
   
   [HttpGet("get")]
   public async Task<IActionResult> GetCart([FromQuery]string sessionId)
   {
      var response = await _cartRepository.GetUserCartByIdAsync(sessionId);
      
      return Ok(new ApiResponse<Cart>(
         "Product retrieved successfully",
         true,
         response
      ));
   }
   
   [HttpPost("add/item")]
   public async Task<IActionResult> AddItemToCart(CartItemDto item, string sessionId)
   {
      var response = await _cartRepository.AddItemToUserCartAsync(item.ToModel(), sessionId);
      
      return Ok(new ApiResponse<Cart>(
         "Item added to cart successfully",
         true,
         response
      ));
   }
   
   [HttpDelete("remove/item")]
   public async Task<IActionResult> RemoveItemFromCart(string sessionId,Guid productId)
   {
      var response = await _cartRepository.RemoveItemFromUserCartAsync(sessionId, productId);
      
      return Ok(new ApiResponse<Cart>(
         "Item removed from cart successfully",
         true,
         response
      ));
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