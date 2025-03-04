using Domain.Models;

namespace Application.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task CreateAsync(RefreshToken refreshToken);
    Task RevokeAsync(RefreshToken token, string? reason = null, string? replacedByToken = null);
}