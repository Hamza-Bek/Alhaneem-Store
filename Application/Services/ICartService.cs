namespace Application.Services;

public interface ICartService
{
    Task<bool> CreateCartAsync(string sessionId);
}