using Application.Dtos.Product;
using Domain.Models;

namespace Application.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<PublicProductDto>> GetProductsAsync();
    Task<Product> GetProductByIdAsync(Guid id);
    Task<IEnumerable<PublicProductDto>> GetNewestProductsAsync();
    Task<IEnumerable<PublicProductDto>> GetLowestPriceProductsAsync();
}