namespace LearnFlowERP.Domain.Entities
{
    public class Refund
    {
        public long RefundId { get; set; }

        public long PaymentId { get; set; }

        public long TenantId { get; set; }

        public decimal Amount { get; set; }

        public string Reason { get; set; } = null!;

        public long? ApprovedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation

        public Payment Payment { get; set; } = null!;

        public User? ApprovedByUser { get; set; }
    }
}