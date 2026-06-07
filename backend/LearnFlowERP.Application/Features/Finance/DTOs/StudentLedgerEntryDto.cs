namespace LearnFlowERP.Application.Features.Finance.DTOs
{
    public class StudentLedgerEntryDto
    {
        public DateTime Date { get; set; }

        public string Type { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        public decimal Balance { get; set; }
    }
}