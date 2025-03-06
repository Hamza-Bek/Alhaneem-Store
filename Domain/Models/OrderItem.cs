namespace Domain.Models;

public class OrderItem : EntityBase
{
    // Quantity Information
    public int Quantity { get; set; }
    
    // Pricing Information
    public decimal Price { get; set; }  // Saves the price at checkout
    public decimal TotalPrice => Price * Quantity;  // Auto-calculated total per item
    
    //Foreign Keys & Navigation Properties
    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; }

    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
}