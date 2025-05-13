using Domain.Enums;

namespace Domain.Models;

public class Product : EntityBase
{
    //Basic Information
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool Published { get; set; }
    
    //Pricing & Discounts
    public decimal Price { get; set; }
    public decimal? Cost { get; set; }

    //Date Information
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; } 
    
    //Inventory
    public int Stock { get; set; }
    public StockStatus StockStatus { get; set; }
    
    //Foreign Keys & Navigation Properties
    public string CategoryId { get; set; } = string.Empty;
    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
}