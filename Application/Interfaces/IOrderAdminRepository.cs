using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;
public interface IOrderAdminRepository
{
    Task<List<Order>> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(Guid id);
    Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatus status);
    Task<bool> UpdateDeliveryStatusAsync(Guid orderId, DeliveryStatus status);
    Task<bool> DeleteOrderAsync(Guid id);
}
