using Domain.Enums;

namespace Domain.Models;

public class Product
{
    public Guid Id { get; set; }
    
    //Basic Information
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    
    //Pricing & Discounts
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
    public decimal DiscountPercentage { get; set; }
    
    //Inventory
    public int Stock { get; set; }
    public StockStatus StockStatus { get; set; }
    
    //Images
    //public string MainImageUrl { get; set; }
    //public List<string> GalleryImages { get; set; }
}