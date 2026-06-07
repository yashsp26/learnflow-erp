using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.FeeReminders.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.FeeReminders.Queries.GetFeeReminderHistory
{
    public class GetFeeReminderHistoryQueryHandler
        : IRequestHandler<
            GetFeeReminderHistoryQuery,
            List<FeeReminderDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetFeeReminderHistoryQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FeeReminderDto>> Handle(
            GetFeeReminderHistoryQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.FeeReminders
                .OrderByDescending(x => x.SentAt)
                .Select(x => new FeeReminderDto
                {
                    FeeReminderId = x.FeeReminderId,
                    FeeId = x.FeeId,
                    Channel = x.Channel,
                    Status = x.Status,
                    FailureReason = x.FailureReason,
                    SentAt = x.SentAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}