using LearnFlowERP.Domain.Enums;
using MediatR;

namespace LearnFlowERP.Application.Features.Notifications.Commands.CreateNotification
{
    public record CreateNotificationCommand(
        List<long> UserIds,
        string Title,
        string Message,
        NotificationType Type,
        NotificationModule Module,
        long? ReferenceId)
        : IRequest<long>;
}