using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Notifications.Commands.MarkAsRead
{
    public class MarkAsReadCommandHandler
        : IRequestHandler<MarkAsReadCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public MarkAsReadCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(
            MarkAsReadCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId!.Value;

            var notification =
                await _context.UserNotifications
                    .FirstOrDefaultAsync(
                        x =>
                            x.NotificationId ==
                                request.NotificationId
                            &&
                            x.UserId == userId,
                        cancellationToken);

            if (notification == null)
            {
                throw new NotFoundException(
                    "Notification not found");
            }

            notification.IsRead = true;
            notification.ReadAt = DateTime.Now;

            await _context.SaveChangesAsync(
                cancellationToken);

            return Unit.Value;
        }
    }
}