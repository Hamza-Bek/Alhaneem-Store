namespace Application.Dtos.Auth;

public class RegisterRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public bool JoinNewsletter { get; set; } // Future : Send new offers and updates to users
}