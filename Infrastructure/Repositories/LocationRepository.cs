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

    public async Task<Location> GetLocationByIdAsync()
    {
        Location location;
        if(_userIdentity.Id != Guid.Empty)
        {
            location  = await _context.Locations
                .FirstOrDefaultAsync(l => l.UserId == _userIdentity.Id);
            
            return location;
        }
        
        var guestSessionId = _httpContextAccessor.HttpContext?.Request.Cookies["GuestSessionId"];
        if (!string.IsNullOrEmpty(guestSessionId))
        {
            location = await _context.Locations
                .FirstOrDefaultAsync(l => l.SessionId == guestSessionId);
            
            return location;
        }

        return null;
    }

    public async Task<Location> AddLocationAsync(Location location)
    {
        Location newLocation;
        
        if (_userIdentity.Id != Guid.Empty)
        {
            newLocation = CreateNewLocation(location, _userIdentity.Id, null);
        }
        else
        {
            var guestSessionId = _httpContextAccessor.HttpContext?.Request.Cookies["GuestSessionId"];
            if (!string.IsNullOrEmpty(guestSessionId))
            {
                newLocation = CreateNewLocation(location, null, guestSessionId);
            }
            else
            {
                return null;
            }
        }
        
        await _context.Locations.AddAsync(newLocation);
        await _context.SaveChangesAsync();
        return newLocation;
    }

    public async Task<Location> UpdateLocationAsync(Location location)
    {
        if (_userIdentity.Id != Guid.Empty)
        {
            var existingLocation = await _context.Locations
                .FirstOrDefaultAsync(l => l.Id == location.Id && l.UserId == _userIdentity.Id);

            if (existingLocation != null)
            {
                UpdateLocationProperties(existingLocation, location);
                await _context.SaveChangesAsync();
                return existingLocation;
            }
        }
        else
        {
            var guestSessionId = _httpContextAccessor.HttpContext?.Request.Cookies["GuestSessionId"];
            if (!string.IsNullOrEmpty(guestSessionId))
            {
                var existingLocation = await _context.Locations
                    .FirstOrDefaultAsync(l => l.Id == location.Id && l.SessionId == guestSessionId);

                if (existingLocation != null)
                {
                    UpdateLocationProperties(existingLocation, location);
                    await _context.SaveChangesAsync();
                    return existingLocation;
                }
            }
        }

        return null;
    }

    public async Task<bool> DeleteLocationAsync(Guid locationId)
    {
        Location location = null;

        if (_userIdentity.Id != Guid.Empty)
        {
            location = await _context.Locations
                .FirstOrDefaultAsync(l => l.Id == locationId && l.UserId == _userIdentity.Id);
        }
        else
        {
            var guestSessionId = _httpContextAccessor.HttpContext?.Request.Cookies["GuestSessionId"];
            if (!string.IsNullOrEmpty(guestSessionId))
            {
                location = await _context.Locations
                    .FirstOrDefaultAsync(l => l.Id == locationId && l.SessionId == guestSessionId);
            }
        }

        if (location != null)
        {
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
    
    private Location CreateNewLocation(Location location, Guid? userId, string sessionId)
    {
        return new Location
        {
            Id = Guid.NewGuid(),
            UserId = userId,
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