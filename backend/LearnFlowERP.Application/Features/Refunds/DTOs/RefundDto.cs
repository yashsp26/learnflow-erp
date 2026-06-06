namespace LearnFlowERP.Application.Features.Refunds.DTOs
{
    public class RefundDto
    {
        public long RefundId { get; set; }

        public long PaymentId { get; set; }

        public decimal Amount { get; set; }

        public string Reason { get; set; } = null!;

        public long? ApprovedBy { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}