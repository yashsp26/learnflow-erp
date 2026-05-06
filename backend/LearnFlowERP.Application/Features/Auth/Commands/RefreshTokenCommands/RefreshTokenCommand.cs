using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnFlowERP.Application.Features.Auth.Commands.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Auth.Commands.RefreshTokenCommands
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<AuthResponseDto>;
}
