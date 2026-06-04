using MediatR;

namespace LearnFlowERP.Application.Features.Permissions.Queries.GetDesignationPermissions
{
    public record GetDesignationPermissionsQuery(
        long DesignationId
    ) : IRequest<List<string>>;
}