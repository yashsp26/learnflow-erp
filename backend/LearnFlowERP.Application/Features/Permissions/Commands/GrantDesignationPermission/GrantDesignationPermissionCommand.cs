using MediatR;

namespace LearnFlowERP.Application.Features.Permissions.Commands.GrantDesignationPermission
{
    public record GrantDesignationPermissionCommand(
    long DesignationId,
    long PermissionId
) : IRequest<Unit>;
}
