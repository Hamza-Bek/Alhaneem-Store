using Application.Interfaces;
using Application.Options;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    private readonly UserIdentity _userIdentity;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppDbContext _context;

    public CartRepository(UserIdentity userIdentity, IHttpContextAccessor httpContextAccessor, AppDbContext context)
    {
        _userIdentity = userIdentity;
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public async Task<Cart> GetUserCartByIdAsync(string sessionId)
    {
        if(string.IsNullOrEmpty(sessionId))
            throw new ArgumentException("Session ID must be provided.");
        
        var cart = await _context.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .ThenInclude(i => i.Images)
            .FirstOrDefaultAsync(c => c.SessionId == sessionId);

        return cart;
    }
    
    public async Task<Cart> CreateUserCartAsync(string sessionId)
    {
        if(string.IsNullOrEmpty(sessionId))
            throw new ArgumentException("Session ID must be provided.");
        
        var existingCart = await _context.Carts
            .FirstOrDefaultAsync(c => c.SessionId.ToString() == sessionId);
        
        if (existingCart != null)
        {
            return existingCart;
        }
        
        var cart = new Cart
        {
            Id = Guid.NewGuid(),
            SessionId = sessionId,
            CreatedAt = DateTime.UtcNow,
            Subtotal = 0,
            ShippingFee = 0,
            DiscountAmount = 0,
            Total = 0,
            IsCheckedOut = false,
        };
        
        await _context.Carts.AddAsync(cart);
        await _context.SaveChangesAsync();

        return cart;
    }

    public async Task<Cart> UpdateItemQuantityAsync(Guid productId, int delta, string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new ArgumentException("Session ID is required.");

        if (delta == 0)
            throw new ArgumentException("Quantity delta cannot be zero.");

        // Get cart with items included
        var cart = await _context.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.SessionId == sessionId);

        if (cart == null)
            throw new InvalidOperationException("Cart not found.");

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (product == null)
            throw new InvalidOperationException("Product not found.");

        var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

        // Adding new item
        if (cartItem == null && delta > 0)
        {
            cartItem = new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cart.Id,
                ProductId = product.Id,
                Quantity = delta,
                Price = product.Price,
                TotalPrice = product.Price * delta
            };

            await _context.CartItems.AddAsync(cartItem);            
        }
        // Modifying existing item
        else if (cartItem != null)
        {
            cartItem.Quantity += delta;

            if (cartItem.Quantity <= 0)
            {
                _context.CartItems.Remove(cartItem);                
            }
            else
            {
                cartItem.TotalPrice = cartItem.Quantity * cartItem.Price;
                _context.CartItems.Update(cartItem);
            }
        }

        // Recalculate cart totals using in-memory list
        cart.Subtotal = cart.Items.Sum(i => i.Quantity * i.Price);
        cart.Total = cart.Subtotal + cart.ShippingFee - cart.DiscountAmount;

        _context.Carts.Update(cart);
        await _context.SaveChangesAsync();

        return cart;
    }

    public async Task<Cart> RemoveItemCompletelyAsync(Guid productId, string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new ArgumentException("Session ID is required.");

        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.SessionId == sessionId);

        if (cart == null)
            throw new InvalidOperationException("Cart not found.");

        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item == null)
            throw new InvalidOperationException("Item not found in cart.");

        _context.CartItems.Remove(item);
        await _context.SaveChangesAsync();

        // Recalculate totals
        cart.Subtotal = cart.Items.Where(i => i.ProductId != productId).Sum(i => i.TotalPrice);
        cart.Total = cart.Subtotal + cart.ShippingFee - cart.DiscountAmount;

        _context.Carts.Update(cart);
        await _context.SaveChangesAsync();

        return await _context.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.Id == cart.Id);
    }

    public async Task<bool> ClearUserCartAsync()
    {
        //Cart userCart;

        //if (_userIdentity.Id != Guid.Empty)
        //{
        //    userCart = await _context.Carts
        //        .Include(c => c.Items)
        //        .FirstOrDefaultAsync(c => c.UserId == _userIdentity.Id);
        //}
        //else
        //{
        //    var guestSessionId = _httpContextAccessor.HttpContext?.Request.Cookies["GuestSessionId"];

        //    if (string.IsNullOrEmpty(guestSessionId))
        //        return false;

        //    userCart = await _context.Carts
        //        .Include(c => c.Items)
        //        .FirstOrDefaultAsync(c => c.SessionId == guestSessionId);
        //}

        //if (userCart is null)
        //    return false;

        //_context.CartItems.RemoveRange(userCart.Items);
        //await _context.SaveChangesAsync();
        //return true;
        throw new Exception();
    }
}