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
        var products = await _context.Products.ToListAsync();
        return products.Select(p => p.ToPublicDto());
    }

    public Task<Product> GetProductByIdAsync(Guid id)
    {
        var product = _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        return product;
    }
}