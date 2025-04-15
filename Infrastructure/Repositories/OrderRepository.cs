using System.Runtime.InteropServices.JavaScript;
using Application.Interfaces;
using Application.Options;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly UserIdentity _userIdentity;
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public OrderRepository(UserIdentity userIdentity, AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _userIdentity = userIdentity;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> SubmitOrderAsync(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new ArgumentException("Session ID is required.");
        
        var hasLocation = await _context.Locations
            .AnyAsync(l => l.SessionId == sessionId);

        if (!hasLocation)
            return false;
        
        var userCart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.SessionId == sessionId && !c.IsCheckedOut);

        if (userCart == null || userCart.Items == null || !userCart.Items.Any())
            return false;
        
        var order = new Order
        {
            Id = Guid.NewGuid(),
            OrderNumber = GenerateOrderNumber(),
            OrderStatus = OrderStatus.Pending,
            DeliveryStatus = DeliveryStatus.Pending,
            Subtotal = userCart.Subtotal,
            ShippingFee = 20,
            DiscountAmount = 0,
            Total = userCart.Subtotal + 20,
            PaymentStatus = PaymentStatus.On_delivery,
            CreatedAt = DateTime.UtcNow,
            SessionId = sessionId,
            Items = userCart.Items.Select(item => new OrderItem
            {
                Id = Guid.NewGuid(),
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price,
                TotalPrice = item.TotalPrice
            }).ToList()
        };

        _context.Orders.Add(order);

        userCart.IsCheckedOut = true;
        userCart.Subtotal = 0;
        userCart.Total = 0;
        _context.CartItems.RemoveRange(userCart.Items);
        _context.Carts.Update(userCart);

        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<Order> GetLastOrderAsync(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new ArgumentException("Session ID is required.");

        return await _context.Orders
            .Where(o => o.SessionId == sessionId)
            .OrderByDescending(o => o.CreatedAt)
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync();
    }

    public Task<Order> GetOrderByIdAsync(int orderId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Order>> GetAllOrdersAsync()
    {
        throw new NotImplementedException();
    }
    
    public static string GenerateOrderNumber()
    {
        string prefix = "ORD"; // You can change this based on category/type
        string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        string uniquePart = Guid.NewGuid().ToString("N").Substring(0, 6); // Shortened GUID for uniqueness
        
        return $"{prefix}-{timestamp}-{uniquePart}";
    }
}