using LearnFlowERP.Application.Features.Permissions.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Permissions.Queries.GetRoles
{
    public record GetRolesQuery()
        : IRequest<List<RoleDto>>;
}
