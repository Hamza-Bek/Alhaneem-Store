using System.Collections;
using Application.Dtos.Product;
using Application.Interfaces;
using Application.Mappers;
using Application.Responses;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet("get/all")]
    public async Task<IActionResult> GetProductsAsync()
    {
        var response = await _productRepository.GetProductsAsync();

        return Ok(new ApiResponse<IEnumerable<PublicProductDto>>(
            "Products retrieved successfully",
            true,
            response
        ));
    }
    
    [HttpGet("get")]
    public async Task<IActionResult> GetProductByIdAsync(Guid id) 
    {
        var response = await _productRepository.GetProductByIdAsync(id);

        return Ok(new ApiResponse<Product>(
            "Product retrieved successfully",
            true,
            response
            ));
    }
    
    [HttpGet("get/newest")]
    public async Task<IActionResult> GetNewestProductsAsync()
    {
        var response = await _productRepository.GetNewestProductsAsync();

        return Ok(new ApiResponse<IEnumerable<PublicProductDto>>(
            "Newest products retrieved successfully",
            true,
            response
        ));
    }
    
    [HttpGet("get/lowest/price")]
    public async Task<IActionResult> GetLowestPriceProductsAsync()
    {
        var response = await _productRepository.GetLowestPriceProductsAsync();

        return Ok(new ApiResponse<IEnumerable<PublicProductDto>>(
            "Lowest price products retrieved successfully",
            true,
            response
        ));
    }
}