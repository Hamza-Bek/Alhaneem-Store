using Domain.Enums;
using Domain.Models;

namespace Application.Dtos.Product;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string CategoryId { get; set; }
    public decimal Price { get; set; }
    public decimal? Cost { get; set; }
    public int Stock { get; set; }
    public StockStatus StockStatus { get; set; }
}