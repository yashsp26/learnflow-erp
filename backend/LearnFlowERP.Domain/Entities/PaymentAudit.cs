namespace LearnFlowERP.Domain.Entities
{
    public class PaymentAudit
    {
        public long PaymentAuditId { get; set; }

        public long PaymentId { get; set; }

        public string? OldStatus { get; set; }

        public string? NewStatus { get; set; }

        public long? ChangedBy { get; set; }

        public DateTime ChangedAt { get; set; } = DateTime.Now;

        // Navigation

        public Payment Payment { get; set; } = null!;

        public User? ChangedByUser { get; set; }
    }
}