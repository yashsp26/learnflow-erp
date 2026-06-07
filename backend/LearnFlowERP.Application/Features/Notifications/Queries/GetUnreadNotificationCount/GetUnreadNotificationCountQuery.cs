using MediatR;

namespace LearnFlowERP.Application.Features.Notifications.Queries.GetUnreadNotificationCount
{
    public record GetUnreadNotificationCountQuery()
        : IRequest<int>;
}