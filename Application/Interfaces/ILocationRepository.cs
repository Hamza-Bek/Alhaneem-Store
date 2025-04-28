using Domain.Models;

namespace Application.Interfaces;

public interface ILocationRepository
{
    Task<Location> GetLocationBySessionAsync(string sessionId);
    Task<Location> AddLocationAsync(Location location, string sessionId);
    Task<Location> UpdateLocationAsync(Location location, string sessionId);
    Task<bool> DeleteLocationAsync(Guid locationId, string sessionId);
}