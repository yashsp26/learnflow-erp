using LearnFlowERP.Domain.Entities;

public class RefreshToken
{
    public long RefreshTokenId { get; set; }

    public long UserId { get; set; }
    public long TenantId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // optional (recommended)
    public string? CreatedByIp { get; set; }

    public User User { get; set; } = null!;
}