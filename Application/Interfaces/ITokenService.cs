using Domain.Models;

namespace Application.Interfaces;

public interface ITokenService
{
    string GenerateJwtToken(ApplicationUser user);
    string GenerateRefreshToken();
}