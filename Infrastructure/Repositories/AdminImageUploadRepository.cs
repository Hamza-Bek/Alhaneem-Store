using Application.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repositories;

public class AdminImageUploadRepository : IAdminImageUploadRepository
{
    private readonly AppDbContext _context;
    private readonly string _imagePath;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AdminImageUploadRepository(AppDbContext context, IOptions<ImageStorageSettings> settings, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _imagePath = settings.Value.ProductImagesPath;
    }

    public async Task UploadImagesAsync(Guid productId, List<IFormFile> files, string uploadPath)
    {
        var productExists = await _context.Products.AnyAsync(p => p.Id == productId);
        if (!productExists)
            throw new Exception("Product not found");

        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);
        
        var request = _httpContextAccessor.HttpContext?.Request;
        if (request == null)
            throw new InvalidOperationException("Unable to resolve HTTP request context.");
        
        var baseUrl = $"{request.Scheme}://{request.Host.Value}";
        
        foreach (var file in files)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            _context.ProductImages.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                ImageUrl = $"{baseUrl}/uploads/{fileName}" // ✅ full absolute URL
            });
        }

        await _context.SaveChangesAsync();
    }
}