using MediatR;

namespace LearnFlowERP.Application.Features.Permissions.Queries.GetRolePermissions
{
    public record GetRolePermissionsQuery(long RoleId)
    : IRequest<List<string>>;
}
