using Application.Dtos.Product;
using Domain.Models;

namespace Application.Mappers;

public static class ProductMapper
{
    public static PublicProductDto ToPublicDto(this Product product)
    {
        return new PublicProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockStatus = product.StockStatus,
            ImageUrls = product.Images?.Select(i => i.ImageUrl).ToList() ?? new()
        };
    }
    
    public static ProductDto ToDto(this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            CategoryId = product.CategoryId,
            Price = product.Price,
            Cost = product.Cost,
            Stock = product.Stock,
            StockStatus = product.StockStatus
        };
    }
    
    public static Product ToModel(this ProductDto product)
    {
        return new Product
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            CategoryId = product.CategoryId,
            Price = product.Price,
            Cost = product.Cost,
            Stock = product.Stock,
            StockStatus = product.StockStatus
        };
    }
}