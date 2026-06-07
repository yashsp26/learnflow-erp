namespace LearnFlowERP.Application.Features.Payments.DTOs
{
    public class ReceiptDto
    {
        public string ReceiptNumber { get; set; } = null!;

        public long PaymentId { get; set; }

        public string StudentName { get; set; } = null!;

        public string FeeType { get; set; } = null!;

        public decimal AmountPaid { get; set; }

        public string PaymentMethod { get; set; } = null!;

        public string? TransactionRef { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}