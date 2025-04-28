using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Product;

public class AdminProductImageUploadDto
{
    public Guid ProductId { get; set; }

    [Required]
    public List<IFormFile> Images { get; set; } = new();
}