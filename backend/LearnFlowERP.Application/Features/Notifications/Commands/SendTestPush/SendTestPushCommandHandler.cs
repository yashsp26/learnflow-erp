using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Enums;
using MediatR;

namespace LearnFlowERP.Application.Features.Notifications.Commands.SendTestPush
{
    public class SendTestPushCommandHandler
        : IRequestHandler<SendTestPushCommand, Unit>
    {
        private readonly INotificationService _notificationService;
        private readonly ICurrentUserService _currentUser;

        public SendTestPushCommandHandler(
            INotificationService notificationService,
            ICurrentUserService currentUser)
        {
            _notificationService = notificationService;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(
            SendTestPushCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId!.Value;

            await _notificationService.SendAsync(
                userId,
                "Test Push Notification",
                "FCM is working successfully.",
                NotificationModule.System);

            return Unit.Value;
        }
    }
}