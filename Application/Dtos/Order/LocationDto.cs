namespace Application.Dtos.Order;

public class LocationDto
{
    public string Name { get; set; } = string.Empty;

    // Basic Information
    public string StreetAddress { get; set; } = string.Empty;
    public string? City { get; set; }
    public string Building { get; set; } = string.Empty;
    public string Floor { get; set; } = string.Empty;
    public string Apartment { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Landmark { get; set; }

    // Geolocation
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? Geolocation { get; set; }

    // Contact Info
    public string PhoneNumber1 { get; set; } = string.Empty;
    public string? PhoneNumber2 { get; set; }

    // Notes
    public string? Notes { get; set; }

    // Required from client for guest users
    public string? SessionId { get; set; }
}