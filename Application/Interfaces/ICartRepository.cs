using Domain.Models;

namespace Application.Interfaces;

public interface ICartRepository 
{
    Task<Cart> GetUserCartByIdAsync(string sessionId);
    Task<Cart> CreateUserCartAsync(string sessionId);
    Task<Cart> AddItemToUserCartAsync(CartItem item, string sessionId);
    Task<Cart> RemoveItemFromUserCartAsync(string sessionId, Guid productId);
    Task<bool> ClearUserCartAsync();
}