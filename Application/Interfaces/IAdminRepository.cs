using Application.Dtos.Product;
using Domain.Models;

namespace Application.Interfaces;

public interface IAdminRepository
{
    /// Product Management
    Task<Product> CreateProductAsync(Product product);
    Task<Product> UpdateProductAsync(Guid productId, Product product);
    Task<bool> DeleteProductAsync(Guid id);
    
    // Category Management
    Task<bool> CreateCategoryAsync(Category category);
    Task<List<Category>> GetAllCategoriesAsync();
    Task<bool> DeleteCategoryAsync(Guid id);
}