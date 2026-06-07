using LearnFlowERP.Domain.Enums;

namespace LearnFlowERP.Domain.Entities
{
    public class FeeReminder
    {
        public long FeeReminderId { get; set; }

        public long FeeId { get; set; }

        public long TenantId { get; set; }

        public ReminderChannel Channel { get; set; }

        public ReminderStatus Status { get; set; }

        public DateTime SentAt { get; set; }

        public string? FailureReason { get; set; }

        public Fee Fee { get; set; } = null!;
    }
}