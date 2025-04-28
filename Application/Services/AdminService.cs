using System.Net.Http.Json;
using Application.Dtos.Product;
using Application.Interfaces;
using Application.Responses;
using Domain.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace Application.Services;

public class AdminService : IAdminService
{
    private readonly HttpClient _httpClient;

    public AdminService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<Product?> CreateProductAsync(ProductDto product)
    {
        var response = await _httpClient.PostAsJsonAsync("api/admin/create", product);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<Product>>();
            return apiResponse?.Data;
        }

        return null;
    }

    public async Task<Product> UpdateProductAsync(Guid productId, ProductDto product)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/admin/update?id={productId}", product);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<Product>>();
            return apiResponse?.Data;
        }

        return null;
    }

    public async Task<bool> DeleteProductAsync(Guid productId)
    {
        var response = await _httpClient.DeleteAsync($"api/admin/delete?id={productId}");

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
            return apiResponse?.Data ?? false;
        }

        return false;
    }

    public async Task<bool> CreateCategoryAsync(CategoryDto category)
    {
        var response = await _httpClient.PostAsJsonAsync("api/admin/category/create", category);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<Product>>();
            return true;
        }

        return false;
    }

    public async Task<List<CategoryDto>> GetAllCategoriesAsync()
    {
        var response = await _httpClient.GetAsync("api/admin/categories");

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<CategoryDto>>>();
            return apiResponse?.Data;
        }

        return null;
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/admin/category/delete?id={id}");

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
            return apiResponse?.Data ?? false;
        }

        return false;
    }

    public async Task<bool> UploadProductImagesAsync(Guid productId, IReadOnlyList<IBrowserFile> files)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(productId.ToString()), "ProductId");

        foreach (var file in files)
        {
            var stream = file.OpenReadStream(10_000_000); // 10MB max
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
            formData.Add(fileContent, "Images", file.Name);
        }

        var response = await _httpClient.PostAsync("api/admin/upload-images", formData);
        return response.IsSuccessStatusCode;
    }
}