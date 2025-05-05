using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class OrderAdminRepository : IOrderAdminRepository
{
    private readonly AppDbContext _context;

    public OrderAdminRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> DeleteOrderAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        var orders = await _context.Orders            
            .Include(i => i.Items)
            .ThenInclude(i => i.Product)
            .Include(i => i.Location)
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync();

        return orders;
    }

    public async Task<Order> GetOrderByIdAsync(Guid id)
    {
        var order = await _context.Orders
            .Include(i => i.Items)
            .ThenInclude(i => i.Product)
            .Include(i => i.Location)
            .FirstOrDefaultAsync(i => i.Id == id);

        return order;
    }

    public async Task<bool> UpdateDeliveryStatusAsync(Guid orderId, DeliveryStatus status)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(i => i.Id == orderId);

        order.DeliveryStatus = status;

        _context.Orders.Update(order);
        var result = await _context.SaveChangesAsync();
        
        if (result == 0)
            return false;

        return true;
    }

    public async Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatus status)
    {
        var order = _context.Orders.FirstOrDefault(i => i.Id == orderId);

        order.OrderStatus = status;

        _context.Orders.Update(order);
        var result = await _context.SaveChangesAsync();
        
        if(result == 0)
            return false;

        return true;
    }
}
