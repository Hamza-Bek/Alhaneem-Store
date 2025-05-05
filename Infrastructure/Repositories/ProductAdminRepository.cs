using Application.Dtos.Product;
using Application.Interfaces;
using Application.Mappers;
using Application.Options;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductAdminRepository : IProductAdminRepository
{
    private readonly AppDbContext _context;

    public ProductAdminRepository(UserIdentity userIdentity, AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<Product>> GetAllProducts()
    {
        var products = await _context.Products
            .Include(i => i.Images)            
            .ToListAsync();

        return products;        
    }

    public async Task<Product> GetProductByIdAsync(Guid id)
    {
        var product = await _context.Products.Where(i => i.Id == id).Include(i => i.Images)
            .FirstOrDefaultAsync();

        return product;
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        // var userId = _userIdentity.Id;
        //
        // if(userId == Guid.Empty)
        //     throw new UnauthorizedAccessException($"User is not authenticated. {userId}");

        var newProduct = new Product()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Published = true,
            CategoryId = product.CategoryId,
            Price = product.Price,
            Cost = product.Cost,
            Stock = product.Stock,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
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
        productToUpdate.CategoryId = product.CategoryId;
        productToUpdate.Price = product.Price;
        productToUpdate.Cost = product.Cost;
        productToUpdate.Stock = product.Stock;
        productToUpdate.StockStatus = product.StockStatus;
        productToUpdate.ModifiedAt = DateTime.UtcNow;
        
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

    #region Category Management

    public async Task<bool> CreateCategoryAsync(Category category)
    {
        var newCategory = new Category()
        {
            Id = category.Id,
            Name = category.Name,
        };
        
        _context.Categories.Add(newCategory);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is null)
            return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
    #endregion
}