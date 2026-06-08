using LearnFlowERP.Domain.Enums;

namespace LearnFlowERP.Domain.Entities
{
    public class UserDevice
    {
        public long UserDeviceId { get; set; }

        public long UserId { get; set; }

        public string DeviceToken { get; set; } = null!;

        public DevicePlatform Platform { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public User User { get; set; } = null!;
    }
}
