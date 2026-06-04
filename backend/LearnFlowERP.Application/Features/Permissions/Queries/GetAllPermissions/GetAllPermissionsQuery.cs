using LearnFlowERP.Application.Features.Permissions.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Permissions.Queries.GetAllPermissions
{
    public record GetAllPermissionsQuery()
    : IRequest<List<PermissionDto>>;
}
