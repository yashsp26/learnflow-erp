using MediatR;

namespace LearnFlowERP.Application.Features.Permissions.Queries.GetCurrentUserPermissions
{
    public record GetCurrentUserPermissionsQuery()
        : IRequest<List<string>>;
}