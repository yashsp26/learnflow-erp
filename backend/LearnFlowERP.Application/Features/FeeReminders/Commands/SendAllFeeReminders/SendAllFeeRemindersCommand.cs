using MediatR;

namespace LearnFlowERP.Application.Features.FeeReminders.Commands.SendAllFeeReminders
{
    public record SendAllFeeRemindersCommand()
        : IRequest<int>;
}