using LearnFlowERP.Domain.Enums;

namespace LearnFlowERP.Domain.Entities
{
    public class Fee : BaseEntity
    {
        public long FeeId { get; set; }

        public long StudentId { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal ScholarshipAmount { get; set; }

        public decimal RefundAmount { get; set; }

        public decimal OutstandingAmount { get; set; }

        public string AcademicYear { get; set; } = null!;

        public DateTime? DueDate { get; set; }

        public FeeStatus Status { get; set; }

        public string FeeType { get; set; } = null!;

        public decimal LateFeeAmount { get; set; }

        public bool IsLateFeeApplied { get; set; }

        // Navigation
        public Student Student { get; set; } = null!;

        public ICollection<Payment> Payments
        { get; set; } = new List<Payment>();

        public ICollection<StudentScholarship> Scholarships
        { get; set; } = new List<StudentScholarship>();
        public void Recalculate()
        {
            OutstandingAmount =
                TotalAmount
                - PaidAmount
                - ScholarshipAmount
                + RefundAmount;

            if (OutstandingAmount < 0)
                OutstandingAmount = 0;

            if (OutstandingAmount == 0)
            {
                Status = FeeStatus.Paid;
            }
            else if (PaidAmount > 0 || ScholarshipAmount > 0)
            {
                Status = FeeStatus.Partial;
            }
            else
            {
                Status = FeeStatus.Pending;
            }
        }

        public bool IsPaid()
        {
            return OutstandingAmount <= 0;
        }
    }
}