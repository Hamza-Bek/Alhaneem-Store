using Application.Dtos.Order;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;
public interface IOrderAdminService
{
    Task<List<OrderDto>> GetAllOrdersAsync();
    Task<OrderDto> GetOrderByIdAsync(Guid id);
    Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatus status);
    Task<bool> UpdateDeliveryStatusAsync(Guid orderId, DeliveryStatus status);
    Task<bool> DeleteOrderAsync(Guid id);
}