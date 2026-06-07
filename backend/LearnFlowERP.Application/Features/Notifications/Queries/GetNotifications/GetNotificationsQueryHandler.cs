using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Notifications.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Notifications.Queries.GetNotifications
{
    public class GetNotificationsQueryHandler
        : IRequestHandler<
            GetNotificationsQuery,
            List<NotificationDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetNotificationsQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<List<NotificationDto>> Handle(
            GetNotificationsQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId!.Value;

            return await _context.UserNotifications
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Notification.CreatedAt)
                .Select(x => new NotificationDto
                {
                    NotificationId =
                        x.Notification.NotificationId,

                    Title =
                        x.Notification.Title,

                    Message =
                        x.Notification.Message,

                    Type =
                        x.Notification.Type,

                    Module =
                        x.Notification.Module,

                    ReferenceId =
                        x.Notification.ReferenceId,

                    IsRead =
                        x.IsRead,

                    CreatedAt =
                        x.Notification.CreatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}