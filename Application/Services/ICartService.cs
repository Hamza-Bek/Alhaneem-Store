namespace Application.Services;

public interface ICartService
{
    Task<bool> CreateCartAsync(string sessionId);
    Task<bool> AddItemToCartAsync(Guid productId, int quantity, string sessionId);
}