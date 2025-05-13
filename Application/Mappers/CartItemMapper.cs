using Application.Dtos.Cart;
using Domain.Models;

namespace Application.Mappers;

public static class CartItemMapper
{
    public static CartItemDto ToDto(this CartItem cartItem)
    {
        return new CartItemDto
        {
            Id = cartItem.Id,
            Quantity = cartItem.Quantity,
            Price = cartItem.Price,
            TotalPrice = cartItem.TotalPrice,
            ProductId = cartItem.ProductId,
            ProductName = cartItem.Product.Name,
            ProductDescription = cartItem.Product?.Description,
            ImageUrls = cartItem.Product.Images?.Select(i => i.ImageUrl).ToList() ?? new()
        };
    }
}