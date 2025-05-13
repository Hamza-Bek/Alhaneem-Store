using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Cart;

public class CartDto
{
    public Guid Id { get; set; } // From EntityBase

    // Pricing Information
    public decimal Subtotal { get; set; }
    public decimal ShippingFee { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal Total { get; set; }

    // Checkout Status
    public bool IsCheckedOut { get; set; }

    // Timestamp
    public DateTime CreatedAt { get; set; }

    // For guests (if applicable)
    public string? SessionId { get; set; }

    // Cart Items
    public List<CartItemDto> Items { get; set; } = new();
}

