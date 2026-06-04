using MediatR;

namespace LearnFlowERP.Application.Features.Permissions.Commands.GrantRolePermission
{
    public record GrantRolePermissionCommand(
    long RoleId,
    long PermissionId
) : IRequest<Unit>;
}
