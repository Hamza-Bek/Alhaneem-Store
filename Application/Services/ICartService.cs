using Application.Dtos.Cart;
using Domain.Models;

namespace Application.Services;

public interface ICartService
{
    Task<CartDto> GetUserCartBySessionIdAsync(string sessionId);
    Task<bool> CreateCartAsync(string sessionId);
    Task<CartDto?> UpdateItemQuantityAsync(Guid productId, int quantityDelta, string sessionId);
    Task<CartDto?> RemoveItemAsync(Guid productId, string sessionId);
}