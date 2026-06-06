namespace LearnFlowERP.Application.Features.Finance.DTOs;

public class CollectionSummaryDto
{
    public decimal TodayCollection { get; set; }

    public decimal ThisMonthCollection { get; set; }

    public decimal ThisYearCollection { get; set; }
}