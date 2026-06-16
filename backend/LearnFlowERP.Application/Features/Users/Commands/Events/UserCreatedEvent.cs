using MediatR;

namespace LearnFlowERP.Application.Features.Users.Commands.Events
{
    public record UserCreatedEvent(string Email, string Username) : INotification;
}
