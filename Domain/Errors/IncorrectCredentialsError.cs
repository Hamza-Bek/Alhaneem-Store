using FluentResults;

namespace Domain.Errors;

public class IncorrectCredentialsError : Error
{
    public IncorrectCredentialsError() : base("Incorrect email or password")
    {
    }
}