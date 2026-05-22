using LearnFlowERP.Domain.Enums;
using MediatR;

namespace LearnFlowERP.Application.Features.Users.Commands
{
    public record CreateUserCommand(
        string Username,
        long RoleId,
        string Email,
        string Password,
        UserType UserType
    ) : IRequest<long>;
}