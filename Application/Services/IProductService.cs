using Application.Dtos.Product;

namespace Application.Services;

public interface IProductService
{
    Task<List<PublicProductDto>> GetAllProductsAsync();
    Task<List<PublicProductDto>> GetNewestProductsAsync();
    
    Task<List<PublicProductDto>> GetLowestPriceProductsAsync();
}