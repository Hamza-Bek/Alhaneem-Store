using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Order
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string OrderStatus { get; set; } = string.Empty;
        public string DeliveryStatus { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public decimal Subtotal { get; set; }
        public decimal ShippingFee { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal Total { get; set; }

        public List<OrderItemDto> Items { get; set; } = new();
        public LocationDto? Location { get; set; }
    }
}
