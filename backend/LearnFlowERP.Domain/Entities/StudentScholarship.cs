namespace LearnFlowERP.Domain.Entities
{
    public class StudentScholarship
    {
        public long StudentScholarshipId { get; set; }

        public long StudentId { get; set; }

        public long FeeId { get; set; }

        public long TenantId { get; set; }

        public string ScholarshipName { get; set; } = null!;

        public decimal Amount { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation

        public Student Student { get; set; } = null!;
        public Fee Fee { get; set; } = null!;

    }
}