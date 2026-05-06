using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnFlowERP.Application.Features.Auth.Commands.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Auth.Commands.Login
{
    public record LoginCommand(
    string Email,
    string Password,
    string TenantCode
) : IRequest<AuthResponseDto>;
}
