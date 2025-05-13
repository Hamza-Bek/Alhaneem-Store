using Application.Dtos.Order;
using Domain.Models;

namespace Application.Mappers;

public static class LocationMapper
{
    public static LocationDto ToDto(this Location location)
    {
        return new LocationDto
        {           
            Name = location.Name,
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
            SessionId = location.SessionId
        };
    }

    public static Location ToModel(this LocationDto dto)
    {
        return new Location
        {
            Name = dto.Name,
            StreetAddress = dto.StreetAddress,
            City = dto.City,
            Building = dto.Building,
            Floor = dto.Floor,
            Apartment = dto.Apartment,
            Address = dto.Address,
            Landmark = dto.Landmark,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            Geolocation = dto.Geolocation,
            PhoneNumber1 = dto.PhoneNumber1,
            PhoneNumber2 = dto.PhoneNumber2,
            Notes = dto.Notes,
            SessionId = dto.SessionId
        };
    }
}