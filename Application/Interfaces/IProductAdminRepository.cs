using Application.Dtos.Product;
using Domain.Models;

namespace Application.Interfaces;

public interface IProductAdminRepository
{
    /// Product Management
    Task<List<Product>> GetAllProducts();
    Task<Product> GetProductByIdAsync(Guid id);
    Task<Product> CreateProductAsync(Product product);
    Task<Product> UpdateProductAsync(Guid productId, Product product);
    Task<bool> DeleteProductAsync(Guid id);
    
    // Category Management
    Task<bool> CreateCategoryAsync(Category category);
    Task<List<Category>> GetAllCategoriesAsync();
    Task<bool> DeleteCategoryAsync(Guid id);
}