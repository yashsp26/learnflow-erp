using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.FeeReminders.Commands.SendAllFeeReminders
{
    public class SendAllFeeRemindersCommandHandler
        : IRequestHandler<
            SendAllFeeRemindersCommand,
            int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFeeReminderService _service;

        public SendAllFeeRemindersCommandHandler(
            IApplicationDbContext context,
            IFeeReminderService service)
        {
            _context = context;
            _service = service;
        }

        public async Task<int> Handle(
            SendAllFeeRemindersCommand request,
            CancellationToken cancellationToken)
        {
            var today = DateTime.Today;

            var fees = await _context.Fees
                .Where(x =>
                    x.OutstandingAmount > 0 &&
                    x.DueDate != null &&
                    x.DueDate <= today &&
                    !_context.FeeReminders.Any(r =>
                        r.FeeId == x.FeeId &&
                        r.Status == ReminderStatus.Sent &&
                        r.SentAt.Date == today))
                .Select(x => x.FeeId)
                .ToListAsync(cancellationToken);

            foreach (var feeId in fees)
            {
                await _service.SendReminderAsync(
                    feeId,
                    cancellationToken);
            }

            return fees.Count;
        }
    }
}