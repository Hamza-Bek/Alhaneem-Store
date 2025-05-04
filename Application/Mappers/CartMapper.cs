using Application.Dtos.Cart;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers;
public static class CartMapper
{
    public static CartDto ToDto(this Cart cart)
    {
        return new CartDto
        {
            Id = cart.Id,
            Subtotal = cart.Subtotal,
            ShippingFee = cart.ShippingFee,
            DiscountAmount = cart.DiscountAmount,
            Total = cart.Total,
            IsCheckedOut = cart.IsCheckedOut,
            CreatedAt = cart.CreatedAt,
            SessionId = cart.SessionId,
            Items = cart.Items?.Select(i => i.ToDto()).ToList() ?? new List<CartItemDto>()
        };
    }
}

