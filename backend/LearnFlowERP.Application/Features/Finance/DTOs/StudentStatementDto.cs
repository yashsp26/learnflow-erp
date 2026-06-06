namespace LearnFlowERP.Application.Features.Finance.DTOs;

public class StudentStatementDto
{
    public long StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public decimal TotalFees { get; set; }

    public decimal PaidAmount { get; set; }

    public decimal ScholarshipAmount { get; set; }

    public decimal RefundAmount { get; set; }

    public decimal OutstandingAmount { get; set; }

    public List<StudentStatementItemDto> History { get; set; }
        = new();
}