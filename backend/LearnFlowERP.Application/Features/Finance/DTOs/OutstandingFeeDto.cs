namespace LearnFlowERP.Application.Features.Finance.DTOs;

public class OutstandingFeeDto
{
    public long StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public decimal TotalFee { get; set; }

    public decimal Outstanding { get; set; }

    public DateTime? DueDate { get; set; }
}