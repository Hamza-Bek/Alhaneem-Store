namespace Domain.Models;

public class Cart : EntityBase
{
    // Pricing Information
    public decimal Subtotal { get; set; }
    public decimal ShippingFee { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal Total { get; set; }

    // Checkout Status
    public bool IsCheckedOut { get; set; }

    // Timestamp
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;     
    public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    
    //Guests
    public string? SessionId { get; set; }
    
}