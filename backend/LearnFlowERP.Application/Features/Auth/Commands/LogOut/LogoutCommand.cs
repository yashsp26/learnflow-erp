using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LearnFlowERP.Application.Features.Auth.Commands.LogOut
{
    public record LogoutCommand(string RefreshToken) : IRequest<bool>;
}
