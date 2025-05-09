using Application.Dtos.Product;
using Application.Interfaces;
using Application.Mappers;
using Application.Responses;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProductsAdminController : ControllerBase
{
    private readonly IProductAdminRepository _adminRepository;
    private readonly IAdminImageUploadRepository _productImageRepository;
    private readonly IWebHostEnvironment _env;
    
    public ProductsAdminController(IProductAdminRepository adminRepository, IAdminImageUploadRepository productImageRepository, IWebHostEnvironment env)
    {
        _adminRepository = adminRepository;
        _productImageRepository = productImageRepository;
        _env = env;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetAllProductsAsync()
    {
        var response = await _adminRepository.GetAllProducts();
        return Ok(new ApiResponse<List<ProductDto>>(
            "Products retrieved successfully",
            true,
            response.Select(p => p.ToDto()).ToList()
        ));
    }

    [HttpGet("product/{id}")]
    public async Task<IActionResult> GetProductByIdAsync(Guid id)
    {
        var response = await _adminRepository.GetProductByIdAsync(id);
        if (response == null)
            return NotFound(new ApiResponse<Product>(
                "Product not found",
                false
            ));
        return Ok(new ApiResponse<ProductDto>(
            "Product retrieved successfully",
            true,
            response.ToDto()
        ));
    }

    [HttpPost("product/create")]
    public async Task<IActionResult> CreateProductAsync(ProductDto product)
    {
        var response = await _adminRepository.CreateProductAsync(product.ToModel());

        return Ok(new ApiResponse<Product>(
            "Product created successfully",
            true,
            response
        ));
    }
    
    [HttpPut("product/update")]
    public async Task<IActionResult> UpdateProductAsync(Guid id, ProductDto product)
    {
        var response = await _adminRepository.UpdateProductAsync(id, product.ToModel());

        return Ok(new ApiResponse<Product>(
            "Product updated successfully",
            true,
            response
        ));
    }

    [HttpDelete("product/delete")]
    public async Task<IActionResult> DeleteProductAsync(Guid id)
    {
        var response = await _adminRepository.DeleteProductAsync(id);

        return Ok(new ApiResponse<bool>(
            "Product deleted successfully",
            true,
            response
        ));
    }
    
    [HttpPost("upload-images")]
    public async Task<IActionResult> UploadImages([FromForm] AdminProductImageUploadDto dto)
    {
        var path = Path.Combine(_env.WebRootPath, "uploads");
        await _productImageRepository.UploadImagesAsync(dto.ProductId, dto.Images, path);
        return Ok();
    }
    
    [HttpPost("category/create")]
    public async Task<IActionResult> CreateCategoryAsync(CategoryDto category)
    {
        var response = await _adminRepository.CreateCategoryAsync(category.ToModel());

        return Ok(new ApiResponse<Category>(
            "Category created successfully",
            true
        ));
    }
    
    [HttpGet("categories")]
    public async Task<IActionResult> GetAllCategoriesAsync()
    {
        var response = await _adminRepository.GetAllCategoriesAsync();

        return Ok(new ApiResponse<List<Category>>(
            "Categories retrieved successfully",
            true,
            response
        ));
    }
    
    [HttpDelete("category/delete")]
    public async Task<IActionResult> DeleteCategoryAsync(Guid id)
    {
        var response = await _adminRepository.DeleteCategoryAsync(id);

        return Ok(new ApiResponse<bool>(
            "Category deleted successfully",
            true,
            response
        ));
    }
}