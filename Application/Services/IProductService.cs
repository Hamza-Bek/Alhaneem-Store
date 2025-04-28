using Application.Dtos.Product;

namespace Application.Services;

public interface IProductService
{
    Task<PublicProductDto> GetAllProductsAsync();
    Task<List<PublicProductDto>> GetNewestProductsAsync();
    
    Task<List<PublicProductDto>> GetLowestPriceProductsAsync();
}