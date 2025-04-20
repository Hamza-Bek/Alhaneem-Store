using System.Net.Http.Json;
using Application.Dtos.Order;

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
        var response = await _httpClient.GetAsync($"api/locations/{sessionId}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<LocationDto>();
        }

        return null;
    }


    public async Task<LocationDto> AddLocationAsync(LocationDto location, string sessionId)
    {
        var payload = new
        {
            LocationDto = location,
            SessionId = sessionId
        };
        
        var response = await _httpClient.PostAsJsonAsync("api/locations", payload);
        if (response.IsSuccessStatusCode)
        {
            var locationResponse = await response.Content.ReadFromJsonAsync<LocationDto>();
            return locationResponse;
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