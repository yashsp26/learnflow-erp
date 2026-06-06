using LearnFlowERP.Domain.Enums;

namespace LearnFlowERP.Domain.Entities
{
    public class Payment
    {
        public long PaymentId { get; set; }

        public long FeeId { get; set; }

        public long TenantId { get; set; }

        public decimal AmountPaid { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public PaymentStatus Status { get; set; }

        public string ReceiptNumber { get; set; } = null!;

        public string? Gateway { get; set; }

        public string? GatewayPaymentId { get; set; }

        public string? TransactionRef { get; set; }

        public string? Remarks { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation

        public Fee Fee { get; set; } = null!;

        public ICollection<PaymentAudit> PaymentAudits
            = new List<PaymentAudit>();

        public ICollection<Refund> Refunds
            = new List<Refund>();
    }
}