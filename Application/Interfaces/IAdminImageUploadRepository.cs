using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IAdminImageUploadRepository
{
    Task UploadImagesAsync(Guid productId, List<IFormFile> files, string uploadPath);
}