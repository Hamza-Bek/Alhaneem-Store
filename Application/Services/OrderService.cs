using Application.Responses;
using Application.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services;
public class OrderService : IOrderService
{

    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> SubmitOrderAsync(string sessionId)
    {
        var response = await _httpClient.PostAsJsonAsync("api/orders/submit",new { sessionId });

        if (response.IsSuccessStatusCode)
            return true;

        var content = await response.Content.ReadAsStringAsync();

        // Just log or inspect without binding to a typed model
        using var doc = JsonDocument.Parse(content);
        if (doc.RootElement.TryGetProperty("errorMessage", out var messageProp))
        {
            var message = messageProp.GetString();
            Console.WriteLine("Order failed: " + message);
        }

        return false;
    }


}
