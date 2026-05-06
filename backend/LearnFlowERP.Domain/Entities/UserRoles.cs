using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Domain.Entities
{
    public class UserRole
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public long TenantId { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.Now;

        // Navigation
        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
