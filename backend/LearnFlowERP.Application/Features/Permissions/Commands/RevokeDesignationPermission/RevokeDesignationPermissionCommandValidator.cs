using FluentValidation;

namespace LearnFlowERP.Application.Features.Permissions.Commands.RevokeDesignationPermission;

public class RevokeDesignationPermissionCommandValidator
    : AbstractValidator<RevokeDesignationPermissionCommand>
{
    public RevokeDesignationPermissionCommandValidator()
    {
        RuleFor(x => x.DesignationId)
            .GreaterThan(0);

        RuleFor(x => x.PermissionId)
            .GreaterThan(0);
    }
}