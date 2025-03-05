using Domain.Models;

namespace Application.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> GetProductByIdAsync(Guid id);
    Task<Product> CreateProductAsync(Product product);
    Task<Product> UpdateProductAsync(Guid id, Product product);
    Task<bool> DeleteProductAsync(Guid id);
}