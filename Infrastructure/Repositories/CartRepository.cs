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

    public async Task<Cart> AddItemToUserCartAsync(CartItem item, string sessionId)
    {
        if(string.IsNullOrEmpty(sessionId))
            throw new ArgumentException("Session ID must be provided.");
        
        var userCart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.SessionId == sessionId);
        
        

        var product = await _context.Products.FirstOrDefaultAsync(i => i.Id == item.ProductId);
        
        var productExists = await _context.CartItems.FirstOrDefaultAsync(p => p.ProductId == item.ProductId && p.CartId == userCart.Id);
        if (productExists != null)
        {
            productExists.Quantity += item.Quantity;
            productExists.TotalPrice = productExists.Price * productExists.Quantity;
            _context.CartItems.Update(productExists);
        }
        else
        {
            var cartItem = new CartItem()
            {
                Id = Guid.NewGuid(),
                CartId = userCart.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = product.Price,
                TotalPrice = product.Price * item.Quantity
            };
            
            await _context.CartItems.AddAsync(cartItem);
        }
        
        userCart.Subtotal = userCart.Items.Sum(i => 
            i.ProductId == item.ProductId
                ? (productExists?.TotalPrice ?? (product.Price * item.Quantity))
                : i.TotalPrice);
        
        userCart.Total = userCart.Subtotal + userCart.ShippingFee;
        
        _context.Carts.Update(userCart);
        await _context.SaveChangesAsync();

        return await _context.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.Id == userCart.Id);
    }

    public async Task<Cart> RemoveItemFromUserCartAsync(string sessionId, Guid productId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new ArgumentException("Session ID is required.");

        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.SessionId == sessionId && !c.IsCheckedOut);

        if (cart == null)
            throw new InvalidOperationException("Cart not found.");

        var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (cartItem == null)
            throw new InvalidOperationException("Cart item not found for given product.");

        if (cartItem.Quantity > 1)
        {
            cartItem.Quantity -= 1;
            cartItem.TotalPrice = cartItem.Quantity * cartItem.Price;
            _context.CartItems.Update(cartItem);
        }
        else
        {
            _context.CartItems.Remove(cartItem);
        }

        await _context.SaveChangesAsync();

        // Recalculate totals
        var remainingItems = await _context.CartItems
            .Where(i => i.CartId == cart.Id)
            .ToListAsync();

        cart.Subtotal = remainingItems.Sum(i => i.TotalPrice);
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
        
        if (userCart is null)
            return false;
        
        _context.CartItems.RemoveRange(userCart.Items);
        await _context.SaveChangesAsync();
        return true;
    }
}