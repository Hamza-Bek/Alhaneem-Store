using Application.Dtos.Product;
using Domain.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace Application.Services;

public interface IProductAdminService
{
    Task<Product?> CreateProductAsync(ProductDto product);
    Task<bool> UploadProductImagesAsync(Guid productId, IReadOnlyList<IBrowserFile> files);
    Task<Product?> UpdateProductAsync(Guid productId, ProductDto product);
    Task<bool> DeleteProductAsync(Guid productId);
    
    Task<bool> CreateCategoryAsync(CategoryDto category);
    Task<List<CategoryDto>> GetAllCategoriesAsync();
    Task<bool> DeleteCategoryAsync(Guid id);
}