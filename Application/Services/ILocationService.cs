using Application.Dtos.Order;

namespace Application.Services;

public interface ILocationService
{
    Task<LocationDto?> GetLocationAsync(string sessionId);
    Task<LocationDto> AddLocationAsync(LocationDto location, string sessionId);
    Task<LocationDto?> UpdateLocationAsync(LocationDto location, string sessionId);
    Task<bool> DeleteLocationAsync(Guid locationId, string sessionId);
}