namespace LearnFlowERP.Application.Features.Finance.DTOs;

public class StudentStatementItemDto
{
    public string Type { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }
}