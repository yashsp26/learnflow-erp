using FluentValidation;

namespace LearnFlowERP.Application.Features.Permissions.Commands.GrantDesignationPermission;

public class GrantDesignationPermissionCommandValidator
    : AbstractValidator<GrantDesignationPermissionCommand>
{
    public GrantDesignationPermissionCommandValidator()
    {
        RuleFor(x => x.DesignationId)
            .GreaterThan(0);

        RuleFor(x => x.PermissionId)
            .GreaterThan(0);
    }
}