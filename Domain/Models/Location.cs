namespace Domain.Models;

public class Location : EntityBase
{
    //Basic Information
    public string? StreetAddress { get; set; }
    public string? City { get; set; }
    public string? Building { get; set; }
    public string? Floor { get; set; }
    public string? Apartment { get; set; }
    public string? Address { get; set; }
    public string? Landmark { get; set; }
    
    //Geolocation
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? Geolocation { get; set; }
    
    //Contact Information
    public string PhoneNumber1 { get; set; }
    public string? PhoneNumber2 { get; set; }
    
    //Notes
    public string? Notes { get; set; }
    
    //Navigation Properties
    public Guid? UserId { get; set; }  // Nullable for guest users
    public virtual ApplicationUser User { get; set; }
    
    //Guests
    public string? SessionId { get; set; }
}