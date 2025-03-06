using Domain.Models;

namespace Application.Interfaces;

public interface ICartRepository 
{
    Task<Cart> GetUserCartByIdAsync();
    Task<Cart> CreateUserCartAsync(string? guestSessionId = null);
    Task<Cart> AddItemToUserCartAsync(CartItem item);
    Task<Cart> RemoveItemFromUserCartAsync(Guid cartItemId);
    Task<bool> ClearUserCartAsync();
}