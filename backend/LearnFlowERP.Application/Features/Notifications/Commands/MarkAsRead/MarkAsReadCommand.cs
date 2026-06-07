using MediatR;

namespace LearnFlowERP.Application.Features.Notifications.Commands.MarkAsRead
{
    public record MarkAsReadCommand(long NotificationId)
        : IRequest<Unit>;
}