namespace LearnFlowERP.Application.Features.Users.DTOs
{
    public class UserDto
    {
        public long UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
