namespace LearnFlowERP.Application.Features.Finance.DTOs;

public class FinanceDashboardDto
{
    public decimal TotalFees { get; set; }

    public decimal TotalCollected { get; set; }

    public decimal OutstandingFees { get; set; }

    public decimal ScholarshipsGiven { get; set; }

    public decimal RefundsIssued { get; set; }

    public int StudentsWithDues { get; set; }
}