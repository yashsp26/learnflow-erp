using LearnFlowERP.Application.Common.Interfaces;
using MediatR;

namespace LearnFlowERP.Application.Features.FeeReminders.Commands.SendFeeReminder
{
    public class SendFeeReminderCommandHandler
        : IRequestHandler<SendFeeReminderCommand, Unit>
    {
        private readonly IFeeReminderService _service;

        public SendFeeReminderCommandHandler(
            IFeeReminderService service)
        {
            _service = service;
        }

        public async Task<Unit> Handle(
            SendFeeReminderCommand request,
            CancellationToken cancellationToken)
        {
            await _service.SendReminderAsync(
                request.FeeId,
                cancellationToken);

            return Unit.Value;
        }
    }
}