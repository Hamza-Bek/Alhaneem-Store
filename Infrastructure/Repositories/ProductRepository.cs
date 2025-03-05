using Application.Interfaces;
using Application.Options;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly UserIdentity _userIdentity;
    private readonly AppDbContext _context;

    public ProductRepository(UserIdentity userIdentity, AppDbContext context)
    {
        _userIdentity = userIdentity;
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        var products = await _context.Products
            .ToListAsync();

        return products;
    }

    public Task<Product> GetProductByIdAsync(Guid id)
    {
        var product = _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        return product;
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        var userId = _userIdentity.Id;
        
        if(userId == Guid.Empty)
            throw new UnauthorizedAccessException($"User is not authenticated. {userId}");

        var newProduct = new Product()
        {
            Id = Guid.NewGuid(),
            Name = product.Name,
            Description = product.Description,
            Category = product.Category,
            Price = product.Price,
            Cost = product.Cost,
            DiscountPercentage = product.DiscountPercentage,
            Stock = product.Stock,
            StockStatus = StockStatus.InStock,
        };
        
        _context.Products.Add(newProduct);
        await _context.SaveChangesAsync();
        
        return newProduct;
    }

    public async Task<Product> UpdateProductAsync(Guid id, Product product)
    {
        var productToUpdate = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        if (productToUpdate == null)
            return null;
        
        productToUpdate.Name = product.Name;
        productToUpdate.Description = product.Description;
        productToUpdate.Category = product.Category;
        productToUpdate.Price = product.Price;
        productToUpdate.Cost = product.Cost;
        productToUpdate.DiscountPercentage = product.DiscountPercentage;
        productToUpdate.Stock = product.Stock;
        productToUpdate.StockStatus = product.StockStatus;
        
        await _context.SaveChangesAsync();

        return productToUpdate;
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}