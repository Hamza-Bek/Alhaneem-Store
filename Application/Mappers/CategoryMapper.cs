using Application.Dtos.Product;
using Domain.Models;

namespace Application.Mappers;

public static class CategoryMapper
{
    public static Category ToModel(this CategoryDto dto)
    {
        return new Category
        {
            Id = dto.Id,
            Name = dto.Name
        };
    }
}