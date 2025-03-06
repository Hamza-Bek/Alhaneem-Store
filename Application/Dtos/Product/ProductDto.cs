using Domain.Enums;
using Domain.Models;

namespace Application.Dtos.Product;

public class ProductDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid? CategoryId { get; set; }
    public decimal Price { get; set; }
    public decimal? Cost { get; set; }
    public int Stock { get; set; }
    public StockStatus StockStatus { get; set; }
}