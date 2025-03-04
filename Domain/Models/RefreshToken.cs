namespace Domain.Models;

public class RefreshToken
{
    public Guid Id { get; set; }  
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string? ReplacedByToken { get; set; }
    public string? RevokedReason { get; set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsRevoked => RevokedAt != null;
    public bool IsActive => !IsRevoked && !IsExpired;
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}