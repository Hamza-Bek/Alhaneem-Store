using Domain.Enums;

namespace Domain.Models;

public class Order : EntityBase
{
    //Order Information
    public string OrderNumber { get; set; } = string.Empty;
    public OrderStatus OrderStatus { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    
    //Order Information
    public decimal Subtotal { get; set; }
    public decimal ShippingFee { get; set; }
    public decimal? DiscountAmount { get; set; }
    public decimal Total { get; set; }
    public PaymentStatus PaymentStatus { get; set; }

    //Foreign Keys & Navigation Properties
    public Guid LocationId { get; set; }
    public Location Location { get; set; } = null!; 
    public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    
    //Guests
    public string? SessionId { get; set; }   
}