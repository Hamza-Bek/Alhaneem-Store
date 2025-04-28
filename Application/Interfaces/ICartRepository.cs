using Domain.Models;

namespace Application.Interfaces;

public interface ICartRepository 
{
    Task<Cart> GetUserCartByIdAsync(string sessionId);
    Task<Cart> CreateUserCartAsync(string sessionId);
    Task<Cart> AddItemToUserCartAsync(Guid productId, int quantity, string sessionId);
    Task<Cart> RemoveItemFromUserCartAsync(Guid productId, string sessionId);
    Task<bool> ClearUserCartAsync();
}