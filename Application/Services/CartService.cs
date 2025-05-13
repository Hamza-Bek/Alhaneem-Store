using Application.Dtos.Cart;
using Application.Responses;
using System.Net.Http.Json;


namespace Application.Services;

public class CartService : ICartService
{
    private readonly HttpClient _httpClient;

    public CartService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CartDto?> GetUserCartBySessionIdAsync(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
            return null;

        var response = await _httpClient.GetFromJsonAsync<ApiResponse<CartDto>>($"api/carts/get/{sessionId}");

        return response?.Data;
    }

    public async Task<bool> CreateCartAsync(string sessionId)
    {
        var response = await _httpClient.PostAsJsonAsync("api/carts/cart", sessionId);
        return response.IsSuccessStatusCode;
    }

    public async Task<CartDto?> UpdateItemQuantityAsync(Guid productId, int quantityDelta, string sessionId)
    {
        var payload = new UpdateCartItemRequest
        {
            ProductId = productId,
            QuantityDelta = quantityDelta,
            SessionId = sessionId
        };

        var response = await _httpClient.PostAsJsonAsync("api/carts/item/update", payload);

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<CartDto>>();
        return result?.Data;
    }

    public async Task<CartDto?> RemoveItemAsync(Guid productId, string sessionId)
    {        
        var response = await _httpClient.DeleteAsync($"api/cart/item?productId={productId}&sessionId={sessionId}");

        if (!response.IsSuccessStatusCode)
            return null;

        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<CartDto>>();
        return apiResponse?.Data;
    }
}