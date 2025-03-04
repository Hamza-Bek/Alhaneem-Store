using Domain.Enums;

namespace Domain.Models;

public class Order
{
    public Guid Id { get; set; }
    
    //Order Information
    public string OrderNumber { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
    
    //Order Information
    public decimal Subtotal { get; set; }
    public decimal ShippingFee { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal Total { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    
    //User Information
    public Guid UserId { get; set; }
    
}