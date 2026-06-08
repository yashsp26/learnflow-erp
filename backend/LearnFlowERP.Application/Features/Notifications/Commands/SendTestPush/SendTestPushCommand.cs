using MediatR;

namespace LearnFlowERP.Application.Features.Notifications.Commands.SendTestPush
{
    public record SendTestPushCommand()
        : IRequest<Unit>;
}