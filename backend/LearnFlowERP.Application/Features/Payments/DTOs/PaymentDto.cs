using LearnFlowERP.Domain.Enums;

namespace LearnFlowERP.Application.Features.Payments.DTOs
{
    public class PaymentDto
    {
        public long PaymentId { get; set; }

        public long FeeId { get; set; }

        public decimal AmountPaid { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public PaymentStatus Status { get; set; }

        public string ReceiptNumber { get; set; } = null!;

        public string? TransactionRef { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}