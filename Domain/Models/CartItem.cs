namespace Domain.Models;

public class CartItem : EntityBase
{
    // Quantity Information
    public int Quantity { get; set; }

    // Pricing Information
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }

    //Foreign Keys & Navigation Properties
    public Guid CartId { get; set; }
    public virtual Cart Cart { get; set; }

    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
}