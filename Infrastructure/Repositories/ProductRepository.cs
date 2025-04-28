using Application.Dtos.Product;
using Application.Interfaces;
using Application.Mappers;
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

    public async Task<IEnumerable<PublicProductDto>> GetProductsAsync()
    {
        var products = await _context.Products
            .Include(i => i.Images)
            .ToListAsync();
        return products.Select(p => p.ToPublicDto());
    }

    public Task<Product> GetProductByIdAsync(Guid id)
    {
        var product = _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        return product;
    }

    public async Task<IEnumerable<PublicProductDto>> GetNewestProductsAsync()
    {
        return await _context.Products.OrderByDescending(p => p.CreatedAt)
            .Take(10)
            .Select(p => new PublicProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockStatus = p.StockStatus,
                ImageUrls = p.Images.Select(i => i.ImageUrl).ToList()
            }).ToListAsync();
    }

    public async Task<IEnumerable<PublicProductDto>> GetLowestPriceProductsAsync()
    {
        return await _context.Products.OrderBy(p => p.Price)
            .Take(10)
            .Select(i => new PublicProductDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Price = i.Price,
                StockStatus = i.StockStatus,
                ImageUrls = i.Images.Select(i => i.ImageUrl).ToList()
            }).ToListAsync();
    }
}