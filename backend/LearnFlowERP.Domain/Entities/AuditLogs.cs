using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Domain.Entities
{
    public class AuditLog
    {
        public long AuditLogId { get; set; }

        public long? TenantId { get; set; }
        public long? UserId { get; set; }

        public string? Action { get; set; }
        public string? TableName { get; set; }
        public long? RecordId { get; set; }

        public string? OldValues { get; set; }
        public string? NewValues { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? CorrelationId { get; set; }

        // Navigation
        public User? User { get; set; }
    }
}
