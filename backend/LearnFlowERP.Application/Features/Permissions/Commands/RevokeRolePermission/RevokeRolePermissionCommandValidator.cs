using FluentValidation;

namespace LearnFlowERP.Application.Features.Permissions.Commands.RevokeRolePermission;

public class RevokeRolePermissionCommandValidator
    : AbstractValidator<RevokeRolePermissionCommand>
{
    public RevokeRolePermissionCommandValidator()
    {
        RuleFor(x => x.RoleId)
            .GreaterThan(0);

        RuleFor(x => x.PermissionId)
            .GreaterThan(0);
    }
}