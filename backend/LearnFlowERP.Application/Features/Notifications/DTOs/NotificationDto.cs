using LearnFlowERP.Domain.Enums;

namespace LearnFlowERP.Application.Features.Notifications.DTOs
{
    public class NotificationDto
    {
        public long NotificationId { get; set; }

        public string Title { get; set; } = null!;

        public string Message { get; set; } = null!;

        public NotificationType Type { get; set; }

        public NotificationModule Module { get; set; }

        public long? ReferenceId { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}