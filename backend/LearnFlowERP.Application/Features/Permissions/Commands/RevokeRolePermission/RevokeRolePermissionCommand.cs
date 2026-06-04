using MediatR;

namespace LearnFlowERP.Application.Features.Permissions.Commands.RevokeRolePermission
{
    public record RevokeRolePermissionCommand(
    long RoleId,
    long PermissionId
) : IRequest<Unit>;
}
