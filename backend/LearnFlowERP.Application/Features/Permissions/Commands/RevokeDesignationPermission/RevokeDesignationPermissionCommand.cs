using MediatR;

namespace LearnFlowERP.Application.Features.Permissions.Commands.RevokeDesignationPermission
{
    public record RevokeDesignationPermissionCommand(
        long DesignationId,
        long PermissionId
    ) : IRequest<Unit>;
}