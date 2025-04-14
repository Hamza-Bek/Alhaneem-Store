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

    public async Task<bool> SubmitOrderAsync()
    {
        Cart userCart;

        if (_userIdentity.Id != Guid.Empty)
        {
            userCart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == _userIdentity.Id);
        }
        else
        {
            var guestSessionId = _httpContextAccessor.HttpContext?.Request.Cookies["GuestSessionId"];
            if (string.IsNullOrEmpty(guestSessionId))
                return false;

            userCart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.SessionId == guestSessionId);
        }

        if (userCart == null || userCart.Items == null || !userCart.Items.Any())
            return false;

        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = _userIdentity.Id != Guid.Empty ? _userIdentity.Id : null,
            OrderNumber = GenerateOrderNumber(),
            OrderStatus = OrderStatus.Pending,
            DeliveryStatus = DeliveryStatus.Pending,
            Subtotal = userCart.Subtotal,
            ShippingFee = 20,
            DiscountAmount = 0,
            Total = userCart.Subtotal + 20,
            PaymentStatus = PaymentStatus.On_delivery,
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
        
        _context.CartItems.RemoveRange(userCart.Items);
        userCart.Subtotal = 0;
        userCart.Total = 0;

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<Order> GetLastOrderAsync()
    {
        if (_userIdentity.Id != Guid.Empty)
        {
            return await _context.Orders
                .Where(o => o.UserId == _userIdentity.Id)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync();
        }
        else
        {
            var guestSessionId = _httpContextAccessor.HttpContext?.Request.Cookies["GuestSessionId"];

            if (!string.IsNullOrEmpty(guestSessionId))
            {
                return await _context.Orders
                    .Where(o => o.SessionId.ToString() == guestSessionId)
                    .OrderByDescending(o => o.CreatedAt)
                    .FirstOrDefaultAsync();
            }
        }
        return null;
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