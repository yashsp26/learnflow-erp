using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlowERP.Application.Features.Permissions.Commands.GrantDesignationPermission
{
    public record GrantDesignationPermissionCommand(
    long DesignationId,
    long PermissionId
) : IRequest<Unit>;
}
