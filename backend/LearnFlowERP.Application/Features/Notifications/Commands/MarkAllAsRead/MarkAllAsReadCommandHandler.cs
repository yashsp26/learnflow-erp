using LearnFlowERP.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Notifications.Commands.MarkAllAsRead
{
    public class MarkAllAsReadCommandHandler
        : IRequestHandler<MarkAllAsReadCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public MarkAllAsReadCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(
            MarkAllAsReadCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId!.Value;

            var notifications =
                await _context.UserNotifications
                    .Where(x =>
                        x.UserId == userId &&
                        !x.IsRead)
                    .ToListAsync(cancellationToken);

            foreach (var item in notifications)
            {
                item.IsRead = true;
                item.ReadAt = DateTime.Now;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}