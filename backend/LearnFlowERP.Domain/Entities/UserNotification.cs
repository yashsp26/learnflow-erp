using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Domain.Entities
{
    public class UserNotification
    {
        public long UserNotificationId { get; set; }

        public long UserId { get; set; }

        public long NotificationId { get; set; }

        public bool IsRead { get; set; }

        public DateTime? ReadAt { get; set; }

        public User User { get; set; } = null!;

        public Notification Notification { get; set; } = null!;
    }
}
