namespace LearnFlowERP.Application.Features.Finance.DTOs;

public class FeeDefaulterDto
{
    public long StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public decimal OutstandingAmount { get; set; }

    public DateTime? DueDate { get; set; }
}