using System.Net.Http.Json;

namespace Application.Services;

public class CartService
{
    private readonly HttpClient _httpClient;

    public CartService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> CreateCartAsync(string sessionId)
    {
        var response = await _httpClient.PostAsJsonAsync("api/carts/create", sessionId);
        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }
}