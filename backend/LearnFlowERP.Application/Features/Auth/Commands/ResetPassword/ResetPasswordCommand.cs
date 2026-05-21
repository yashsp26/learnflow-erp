using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Application.Features.Auth.Commands.ResetPassword
{
    public record ResetPasswordCommand(
        string Email,
        string Otp,
        string NewPassword
    ) : IRequest<Unit>;
}
