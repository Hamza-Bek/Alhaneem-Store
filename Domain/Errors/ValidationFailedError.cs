using FluentResults;

namespace Domain.Errors;

public class ValidationFailedError : Error
{
    public Dictionary<string, string[]> Errors { get; }

    public ValidationFailedError(Dictionary<string, string[]> errors) : base("Validation failed")
    {
        Errors = errors;
    }
}