using System.Net.Http.Json;

namespace Application.Services;

public class CartService : ICartService
{
    private readonly HttpClient _httpClient;

    public CartService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> CreateCartAsync(string sessionId)
    {
        var response = await _httpClient.PostAsJsonAsync("api/carts/cart", sessionId);
        return response.IsSuccessStatusCode;
    }
    
    public async Task<bool> AddItemToCartAsync(Guid productId, int quantity, string sessionId)
    {
        var payload = new
        {
            ProductId = productId,
            Quantity = quantity,
            SessionId = sessionId
        };

        var response = await _httpClient.PostAsJsonAsync("api/carts/item", payload);
        return response.IsSuccessStatusCode;
    }
    
    public async Task<bool> RemoveItemFromCartAsync(Guid productId, string sessionId)
    {
        var payload = new
        {
            ProductId = productId,
            SessionId = sessionId
        };

        var response = await _httpClient.PostAsJsonAsync("api/carts/remove/item", payload);
        return response.IsSuccessStatusCode;
    }
}