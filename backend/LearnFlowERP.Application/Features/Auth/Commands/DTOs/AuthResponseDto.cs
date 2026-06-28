namespace LearnFlowERP.Application.Features.Auth.Commands.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public long UserId { get; set; }
        public string Email { get; set; } = null!;
    }
}
