using LearnFlowERP.Domain.Enums;

namespace LearnFlowERP.Domain.Entities
{
    public class Notification
    {
        public long NotificationId { get; set; }

        public long TenantId { get; set; }

        public string Title { get; set; } = null!;

        public string Message { get; set; } = null!;

        public NotificationType Type { get; set; }

        public NotificationModule Module { get; set; }

        public long? ReferenceId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<UserNotification>
            UserNotifications
        { get; set; }
            = new List<UserNotification>();
    }
}