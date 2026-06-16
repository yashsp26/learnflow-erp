using FluentValidation;

namespace LearnFlowERP.Application.Features.Permissions.Queries.GetDesignationPermissions;

public class GetDesignationPermissionsQueryValidator
    : AbstractValidator<GetDesignationPermissionsQuery>
{
    public GetDesignationPermissionsQueryValidator()
    {
        RuleFor(x => x.DesignationId)
            .GreaterThan(0);
    }
}