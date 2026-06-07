using LearnFlowERP.Application.Features.Notifications.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Notifications.Queries.GetNotifications
{
    public record GetNotificationsQuery()
        : IRequest<List<NotificationDto>>;
}