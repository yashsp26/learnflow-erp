using LearnFlowERP.Domain.Enums;

namespace LearnFlowERP.Application.Features.FeeReminders.DTOs
{
    public class FeeReminderDto
    {
        public long FeeReminderId { get; set; }

        public long FeeId { get; set; }

        public ReminderChannel Channel { get; set; }

        public ReminderStatus Status { get; set; }

        public string? FailureReason { get; set; }

        public DateTime SentAt { get; set; }
    }
}