using FluentResults;

namespace Domain.Errors;

public class UserCreationFailedError : Error
{
    public IEnumerable<string> Errors { get; set; }

    public UserCreationFailedError(IEnumerable<string> errors) : base("User creation failed")
    {
        Errors = errors;
    }
}