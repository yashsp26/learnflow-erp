using FluentValidation;

namespace LearnFlowERP.Application.Features.Permissions.Queries.GetRolePermissions;

public class GetRolePermissionsQueryValidator
    : AbstractValidator<GetRolePermissionsQuery>
{
    public GetRolePermissionsQueryValidator()
    {
        RuleFor(x => x.RoleId)
            .GreaterThan(0);
    }
}