using Domain.Enums;

namespace Application.Dtos.Product;

public class PublicProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public StockStatus StockStatus { get; set; }
    public List<string> ImageUrls { get; set; } = new();
}