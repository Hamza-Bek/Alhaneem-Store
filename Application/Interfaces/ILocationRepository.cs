using Domain.Models;

namespace Application.Interfaces;

public interface ILocationRepository
{
    Task<Location> GetLocationByIdAsync();
    Task<Location> AddLocationAsync(Location location);
    Task<Location> UpdateLocationAsync(Location location);
    Task<bool> DeleteLocationAsync(Guid locationId);
}