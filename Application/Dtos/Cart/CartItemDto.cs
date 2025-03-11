namespace Application.Dtos.Cart;

public class CartItemDto
{
    // Quantity Information
    public int Quantity { get; set; } 
    
    // Pricing Information
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }

    //Foreign Keys & Navigation Properties
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; } 
}