using LearnFlowERP.Domain.Enums;

namespace LearnFlowERP.Application.Features.Fees.DTOs
{
    public class FeeDto
    {
        public long FeeId { get; set; }

        public long StudentId { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal PendingAmount { get; set; }

        public string FeeType { get; set; } = null!;

        public string AcademicYear { get; set; } = null!;

        public FeeStatus Status { get; set; }

        public DateTime? DueDate { get; set; }
    }
}