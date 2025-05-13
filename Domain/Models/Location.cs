namespace Domain.Models;

public class Location : EntityBase
{
    public string Name { get; set; } = string.Empty;
    //Basic Information
    public string StreetAddress { get; set; } = string.Empty;
    public string? City { get; set; }
    public string Building { get; set; }  = string.Empty;
    public string Floor { get; set; }  = string.Empty;
    public string Apartment { get; set; }  = string.Empty;
    public string Address { get; set; }  = string.Empty;
    public string? Landmark { get; set; }
    
    //Geolocation
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? Geolocation { get; set; }
    
    //Contact Information
    public string PhoneNumber1 { get; set; }  = string.Empty;
    public string? PhoneNumber2 { get; set; }
    
    //Notes
    public string? Notes { get; set; }

    //Guests
    public string SessionId { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();

}