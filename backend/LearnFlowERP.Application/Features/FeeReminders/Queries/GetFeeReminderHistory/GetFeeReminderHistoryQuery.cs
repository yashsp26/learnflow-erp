using LearnFlowERP.Application.Features.FeeReminders.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.FeeReminders.Queries.GetFeeReminderHistory
{
    public record GetFeeReminderHistoryQuery()
        : IRequest<List<FeeReminderDto>>;
}