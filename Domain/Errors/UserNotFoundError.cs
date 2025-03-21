using FluentResults;

namespace Domain.Errors;

public class UserNotFoundError : Error
{
    public UserNotFoundError() : base("User not found")
    {
    }
}

public class UserAlreadyExistsError : Error
{
    public UserAlreadyExistsError() : base("User already exists")
    {
    }
}