using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LearnFlowERP.Application.Features.Users.Commands
{
    public record CreateUserCommand(
    string Username,
    long RoleId,
    string Email,
    string Password
) : IRequest<long>;
}
