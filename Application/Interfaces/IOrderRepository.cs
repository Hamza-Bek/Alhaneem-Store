using Domain.Models;
using FluentResults;

namespace Application.Interfaces;

public interface IOrderRepository
{
    Task<bool> SubmitOrderAsync(string sessionId);
    Task<Order> GetLastOrderAsync(string sessionId);
    Task<Order> GetOrderByIdAsync(int orderId);
    Task<List<Order>> GetAllOrdersAsync();
}