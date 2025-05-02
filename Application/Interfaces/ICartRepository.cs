using Domain.Models;

namespace Application.Interfaces;

public interface ICartRepository 
{
    Task<Cart> GetUserCartByIdAsync(string sessionId);
    Task<Cart> CreateUserCartAsync(string sessionId);
    Task<Cart> RemoveItemCompletelyAsync(Guid productId, string sessionId);
    Task<Cart> UpdateItemQuantityAsync(Guid productId, int delta, string sessionId);
    Task<bool> ClearUserCartAsync();
}