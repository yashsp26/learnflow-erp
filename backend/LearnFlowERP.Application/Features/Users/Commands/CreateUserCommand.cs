using MediatR;

namespace LearnFlowERP.Application.Features.Users.Commands
{
    public record CreateUserCommand(
        string Username,
        long RoleId,
        string Email
    ) : IRequest<long>;
}