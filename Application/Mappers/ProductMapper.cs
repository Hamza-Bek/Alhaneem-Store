using Application.Dtos.Product;
using Domain.Models;

namespace Application.Mappers;

public static class ProductMapper
{
    public static ProductDto ToDto(this Product product)
    {
        return new ProductDto
        {
            Name = product.Name,
            Description = product.Description,
            Category = product.Category,
            Price = product.Price,
            Cost = product.Cost,
            DiscountPercentage = product.DiscountPercentage,
            Stock = product.Stock,
            StockStatus = product.StockStatus
        };
    }
    
    public static Product ToModel(this ProductDto product)
    {
        return new Product
        {
            Name = product.Name,
            Description = product.Description,
            Category = product.Category,
            Price = product.Price,
            Cost = product.Cost,
            DiscountPercentage = product.DiscountPercentage,
            Stock = product.Stock,
            StockStatus = product.StockStatus
        };
    }
}