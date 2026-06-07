using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using MediatR;

namespace LearnFlowERP.Application.Features.Notifications.Commands.CreateNotification
{
    public class CreateNotificationCommandHandler
        : IRequestHandler<CreateNotificationCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CreateNotificationCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<long> Handle(
            CreateNotificationCommand request,
            CancellationToken cancellationToken)
        {
            var notification = new Notification
            {
                TenantId = _currentUser.TenantId!.Value,
                Title = request.Title,
                Message = request.Message,
                Type = request.Type,
                Module = request.Module,
                ReferenceId = request.ReferenceId
            };

            foreach (var userId in request.UserIds.Distinct())
            {
                notification.UserNotifications.Add(
                    new UserNotification
                    {
                        UserId = userId,
                        IsRead = false
                    });
            }

            _context.Notifications.Add(notification);

            await _context.SaveChangesAsync(cancellationToken);

            return notification.NotificationId;
        }
    }
}