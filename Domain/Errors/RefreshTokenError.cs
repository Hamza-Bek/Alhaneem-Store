using FluentResults;

namespace Domain.Errors;

public class RefreshTokenError : Error
{
    public RefreshTokenError(string message) : base(message)
    {
    }
}