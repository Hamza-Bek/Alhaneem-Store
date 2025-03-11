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
   
   [HttpGet("get")]
   public async Task<IActionResult> GetCart()
   {
      var response = await _cartRepository.GetUserCartByIdAsync();
      
      return Ok(new ApiResponse<Cart>(
         "Product retrieved successfully",
         true,
         response
      ));
   }
   
   [HttpPost("add/item")]
   public async Task<IActionResult> AddItemToCart(CartItemDto item)
   {
      var response = await _cartRepository.AddItemToUserCartAsync(item.ToModel());
      
      return Ok(new ApiResponse<Cart>(
         "Item added to cart successfully",
         true,
         response
      ));
   }
   
   [HttpDelete("remove/item")]
   public async Task<IActionResult> RemoveItemFromCart(Guid id)
   {
      var response = await _cartRepository.RemoveItemFromUserCartAsync(id);
      
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