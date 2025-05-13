using Application.Dtos.Cart;
using Application.Dtos.Order;
using Application.Responses;
using System.Net.Http.Json;

namespace Application.Services;

public class LocationService : ILocationService
{
    private readonly HttpClient _httpClient;

    public LocationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LocationDto?> GetLocationAsync(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
            return null;

        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<LocationDto>>($"api/locations/{sessionId}");
            return response?.Data;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"[LocationService] Error fetching location: {ex.Message}");
            return null;
        }
    }



    public async Task<LocationDto?> AddLocationAsync(LocationDto location, string sessionId)
    {
        location.SessionId = sessionId;

        var response = await _httpClient.PostAsJsonAsync("api/locations", location);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<LocationDto>();
        }

        return null;
    }

    public async Task<LocationDto?> UpdateLocationAsync(LocationDto location, string sessionId)
    {
        var payload = new
        {
            Location = location,
            SessionId = sessionId
        };

        var response = await _httpClient.PutAsJsonAsync("api/locations/update", payload);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<LocationDto>();
        }

        return null;
    }
    
    public async Task<bool> DeleteLocationAsync(Guid locationId, string sessionId)
    {
        var payload = new
        {
            LocationId = locationId,
            SessionId = sessionId
        };

        var response = await _httpClient.PostAsJsonAsync("api/locations/delete", payload);
        return response.IsSuccessStatusCode;
    }

}