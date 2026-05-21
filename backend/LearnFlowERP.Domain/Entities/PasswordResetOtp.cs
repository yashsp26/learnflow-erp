using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Domain.Entities
{
    public class PasswordResetOtp
    {
        public long PasswordResetOtpId { get; set; }

        public string Email { get; set; } = null!;

        public string Otp { get; set; } = null!;

        public DateTime ExpiresAt { get; set; }

        public bool IsUsed { get; set; }
    }
}
