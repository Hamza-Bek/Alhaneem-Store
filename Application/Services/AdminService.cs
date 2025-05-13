using Application.Dtos.Admin;
using System.Net.Http.Json;

namespace Application.Services;

public class AdminService : IAdminService
{
    private readonly HttpClient _httpClient;

    public AdminService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<SalesStatisticsDto> GetSalesStatisticsAsync(DateTime? date)
    {
        var response = await _httpClient.GetFromJsonAsync<SalesStatisticsDto>("api/admin/sales/statistics");

        return response;
    }
}
