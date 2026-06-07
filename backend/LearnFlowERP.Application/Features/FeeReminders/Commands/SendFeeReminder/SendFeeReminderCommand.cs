using MediatR;

namespace LearnFlowERP.Application.Features.FeeReminders.Commands.SendFeeReminder
{
    public record SendFeeReminderCommand(long FeeId)
        : IRequest<Unit>;
}