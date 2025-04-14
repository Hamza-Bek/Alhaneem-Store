using Application.Interfaces;
using Application.Options;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
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

    public async Task<Cart> GetUserCartByIdAsync()
    {
        if (_userIdentity.Id != Guid.Empty)
        {
            var cart = await _context.Carts
                .Include(i => i.Items)
                .ThenInclude(i => i.Product)
                .Include(i => i.User)
                .FirstOrDefaultAsync(c => c.UserId == _userIdentity.Id);

            return cart;
        }
        else
        {
            var guestSessionId = _httpContextAccessor.HttpContext?.Request.Cookies["GuestSessionId"];
            if (!string.IsNullOrEmpty(guestSessionId))
            {
                return await _context.Carts
                    .Include(i => i.Items)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync(c => c.SessionId.ToString() == guestSessionId);
            }
        }

        return null;
    }
    public async Task<Cart> CreateUserCartAsync(string? guestSessionId = null)
    {
        if (_userIdentity.Id != Guid.Empty)
        {
            var cart = new Cart()
            {
                Id = Guid.NewGuid(),
                UserId = _userIdentity.Id,
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
        else
        {
            guestSessionId = _httpContextAccessor.HttpContext?.Request.Cookies["GuestSessionId"];
            
            if (string.IsNullOrEmpty(guestSessionId))
            {
                // Create a new guest session ID
                guestSessionId = Guid.NewGuid().ToString();
                _httpContextAccessor.HttpContext?.Response.Cookies.Append("GuestSessionId", guestSessionId);
            }
            
            var existingCart = await _context.Carts.FirstOrDefaultAsync(c => c.SessionId == guestSessionId);
            if (existingCart != null)
            {
                return existingCart;
            }
            
            var cart = new Cart()
            {
                Id = Guid.NewGuid(),
                SessionId = guestSessionId,
                UserId = null,
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
        
        return null;
    }

    public async Task<Cart> AddItemToUserCartAsync(CartItem item)
    {
        Cart userCart;
        if (_userIdentity.Id != Guid.Empty)
        {
            userCart = await _context.Carts.FirstOrDefaultAsync(i => i.UserId == _userIdentity.Id)
                       ?? await CreateUserCartAsync();
            
            var cartItem = new CartItem()
            {
                Id = Guid.NewGuid(),
                CartId = userCart.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price,
                TotalPrice = item.Price * item.Quantity
            };
            
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }
        else
        {
            var guestSessionId = _httpContextAccessor.HttpContext?.Request.Cookies["GuestSessionId"];

            if (string.IsNullOrEmpty(guestSessionId))
            {
                guestSessionId = Guid.NewGuid().ToString();
                _httpContextAccessor.HttpContext?.Response.Cookies.Append("GuestSessionId", guestSessionId);
            }
            
            userCart = await _context.Carts.FirstOrDefaultAsync(i => i.SessionId == guestSessionId)
                       ?? await CreateUserCartAsync(guestSessionId);
            
            var cartItem = new CartItem()
            {
                Id = Guid.NewGuid(),
                CartId = userCart.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price,
                TotalPrice = item.Price * item.Quantity
            };
                
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }

        return await _context.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.Id == userCart.Id);
    }

    public async Task<Cart> RemoveItemFromUserCartAsync(Guid cartItemId)
    {
        Cart userCart;
        if(_userIdentity.Id != Guid.Empty)
        {
            userCart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == _userIdentity.Id);
        }
        else
        {
            var guestSessionId = _httpContextAccessor.HttpContext?.Request.Cookies["GuestSessionId"];

            if (string.IsNullOrEmpty(guestSessionId))
                return null; // No guest cart exists

            userCart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.SessionId == guestSessionId);
        }
        
        if (userCart is null)
            return null; 

        var cartItem = userCart.Items.FirstOrDefault(i => i.Id == cartItemId);
        if (cartItem is null)
            return null; 

        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();

        return userCart; 
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