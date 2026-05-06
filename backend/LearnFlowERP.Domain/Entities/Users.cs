using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Domain.Entities
{
    public class User : BaseEntity
    {
        public long UserId { get; set; }

        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public DateTime? DeletedAt { get; set; }
        public string? ProfileImageUrl { get; set; }

        // Navigation
        public Tenant Tenant { get; set; } = null!;
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public Student? Student { get; set; }
        public Employee? Employee { get; set; }
    }
}
