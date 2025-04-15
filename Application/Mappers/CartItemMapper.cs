using Application.Dtos.Cart;
using Application.Dtos.Product;
using Domain.Models;

namespace Application.Mappers;

public static class CartItemMapper
{
    public static CartItemDto ToDto(this CartItem cart)
    {
        return new CartItemDto
        {
            Quantity =  cart.Quantity,
            ProductId = cart.ProductId
        };
    }

    public static CartItem ToModel(this CartItemDto cart)
    {
        return new CartItem
        {
            Quantity =  cart.Quantity,
            ProductId = cart.ProductId
        };
    }
}