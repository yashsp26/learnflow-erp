
using LearnFlowERP.Domain.Enums;
namespace LearnFlowERP.Domain.Entities
{
    public class User : BaseEntity
    {
        public long UserId { get; set; }

        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public bool ProfileCompleted { get; set; } = false;

        public DateTime? DeletedAt { get; set; }
        public string? ProfileImageUrl { get; set; }

        public UserType UserType { get; set; }

        // Navigation
        public Tenant Tenant { get; set; } = null!;
        public Student? Student { get; set; }
        public Employee? Employee { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();

    }
}
