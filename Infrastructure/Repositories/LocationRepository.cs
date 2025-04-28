using Application.Interfaces;
using Application.Options;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly UserIdentity _userIdentity;
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LocationRepository(UserIdentity userIdentity, IHttpContextAccessor httpContextAccessor, AppDbContext context)
    {
        _userIdentity = userIdentity;
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public async Task<Location?> GetLocationBySessionAsync(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new ArgumentException("Session ID is required.");

        var location = await _context.Locations
            .FirstOrDefaultAsync(l => l.SessionId == sessionId);

        return location;
    }


    public async Task<Location> AddLocationAsync(Location location, string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new ArgumentException("Session ID is required.");

        var newLocation = new Location
        {
            Id = Guid.NewGuid(),
            SessionId = sessionId,
            Name = location.Name,
            Address = location.Address,
            PhoneNumber1 = location.PhoneNumber1,
            PhoneNumber2 = location.PhoneNumber2,
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            CreatedAt = DateTime.UtcNow,
            StreetAddress = location.StreetAddress,
            City = location.City,
            Building = location.Building,
            Floor = location.Floor,
            Apartment = location.Apartment,
            Landmark = location.Landmark,
            Geolocation = location.Geolocation,
            Notes = location.Notes,
        };

        await _context.Locations.AddAsync(newLocation);
        await _context.SaveChangesAsync();

        return newLocation;
    }

    public async Task<Location?> UpdateLocationAsync(Location location, string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new ArgumentException("Session ID is required.");

        var existingLocation = await _context.Locations
            .FirstOrDefaultAsync(l => l.Id == location.Id && l.SessionId == sessionId);

        if (existingLocation == null)
            return null;

        UpdateLocationProperties(existingLocation, location);
        await _context.SaveChangesAsync();

        return existingLocation;
    }

    public async Task<bool> DeleteLocationAsync(Guid locationId, string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new ArgumentException("Session ID is required.");

        var location = await _context.Locations
            .FirstOrDefaultAsync(l => l.Id == locationId && l.SessionId == sessionId);

        if (location == null)
            return false;

        _context.Locations.Remove(location);
        await _context.SaveChangesAsync();
        return true;
    }

    
    private Location CreateNewLocation(Location location, Guid? userId, string sessionId)
    {
        return new Location
        {
            Id = Guid.NewGuid(),
            StreetAddress = location.StreetAddress,
            City = location.City,
            Building = location.Building,
            Floor = location.Floor,
            Apartment = location.Apartment,
            Address = location.Address,
            Landmark = location.Landmark,
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            Geolocation = location.Geolocation,
            PhoneNumber1 = location.PhoneNumber1,
            PhoneNumber2 = location.PhoneNumber2,
            Notes = location.Notes,
            SessionId = sessionId
        };
    }
    
    private void UpdateLocationProperties(Location existingLocation, Location updatedLocation)
    {
        existingLocation.StreetAddress = updatedLocation.StreetAddress;
        existingLocation.City = updatedLocation.City;
        existingLocation.Building = updatedLocation.Building;
        existingLocation.Floor = updatedLocation.Floor;
        existingLocation.Apartment = updatedLocation.Apartment;
        existingLocation.Address = updatedLocation.Address;
        existingLocation.Landmark = updatedLocation.Landmark;
        existingLocation.Latitude = updatedLocation.Latitude;
        existingLocation.Longitude = updatedLocation.Longitude;
        existingLocation.Geolocation = updatedLocation.Geolocation;
        existingLocation.PhoneNumber1 = updatedLocation.PhoneNumber1;
        existingLocation.PhoneNumber2 = updatedLocation.PhoneNumber2;
        existingLocation.Notes = updatedLocation.Notes;
    }
}