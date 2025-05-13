using Application.Dtos.Product;

namespace Application.Dtos.Cart;

public class CartItemDto
{
    public Guid Id { get; set; } // From EntityBase

    // Quantity Information
    public int Quantity { get; set; }

    // Pricing Information
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }

    // Product Info
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public List<string> ImageUrls { get; set; } = new();
}