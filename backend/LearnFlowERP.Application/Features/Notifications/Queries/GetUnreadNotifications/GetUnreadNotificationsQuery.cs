using LearnFlowERP.Application.Features.Notifications.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Notifications.Queries.GetUnreadNotifications
{
    public record GetUnreadNotificationsQuery()
        : IRequest<List<NotificationDto>>;
}