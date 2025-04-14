using Domain.Models;
using FluentResults;

namespace Application.Interfaces;

public interface IOrderRepository
{
    Task<bool> SubmitOrderAsync();
    Task<Order> GetLastOrderAsync();
    Task<Order> GetOrderByIdAsync(int orderId);
    Task<List<Order>> GetAllOrdersAsync();
}