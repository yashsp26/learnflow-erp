namespace LearnFlowERP.Application.Features.Scholarships.DTOs
{
    public class ScholarshipDto
    {
        public long StudentScholarshipId { get; set; }

        public long StudentId { get; set; }

        public string ScholarshipName { get; set; } = null!;

        public decimal Amount { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public bool IsActive { get; set; }
    }
}