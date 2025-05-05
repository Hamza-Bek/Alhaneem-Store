using Application.Dtos.Order;
using Application.Responses;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;
public class OrderAdminService : IOrderAdminService
{
    private readonly HttpClient _httpClient;

    public OrderAdminService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<bool> DeleteOrderAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<OrderDto>> GetAllOrdersAsync()
    {
        var response = await _httpClient.GetAsync("api/ordersadmin/orders");
        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<OrderDto>>>();
            return apiResponse?.Data ?? new List<OrderDto>();
        }
        return new List<OrderDto>();
    }

    public async Task<OrderDto> GetOrderByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"api/ordersadmin/order/{id}");
        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<OrderDto>>();
            return apiResponse?.Data;
        }
        return null;
    }

    public async Task<bool> UpdateDeliveryStatusAsync(Guid orderId, DeliveryStatus status)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/ordersadmin/order/delivery/update?orderId={orderId}&status={status}", new { orderId, status });
        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
            return apiResponse?.Data ?? false;
        }
        return false;
    }

    public async Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatus status)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/ordersadmin/order/update?orderId={orderId}&status={status}", new { orderId, status });
        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
            return apiResponse?.Data ?? false;
        }
        return false;
    }
}
