using Domain.Enums;

namespace Application.Dtos.Product;

public class ProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
    public decimal DiscountPercentage { get; set; }
    public int Stock { get; set; }
    public StockStatus StockStatus { get; set; }
}