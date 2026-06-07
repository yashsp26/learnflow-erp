using MediatR;

namespace LearnFlowERP.Application.Features.Notifications.Commands.MarkAllAsRead
{
    public record MarkAllAsReadCommand()
        : IRequest<Unit>;
}