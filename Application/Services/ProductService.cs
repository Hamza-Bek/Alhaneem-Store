using System.Net.Http.Json;
using Application.Dtos.Product;
using Application.Responses;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<PublicProductDto>> GetAllProductsAsync()
    {        
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<PublicProductDto>>>("api/Products/get/all");
        return response?.Data ?? new();
    }

    public async Task<List<PublicProductDto>> GetNewestProductsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<PublicProductDto>>>("api/Products/get/newest");
        return response?.Data ?? new();
    }

    public async Task<List<PublicProductDto>> GetLowestPriceProductsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<PublicProductDto>>>("api/products/get/lowest/price");
        return response?.Data ?? new();
    }
}