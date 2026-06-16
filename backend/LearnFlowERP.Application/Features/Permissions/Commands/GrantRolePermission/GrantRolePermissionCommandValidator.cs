using FluentValidation;

namespace LearnFlowERP.Application.Features.Permissions.Commands.GrantRolePermission;

public class GrantRolePermissionCommandValidator
    : AbstractValidator<GrantRolePermissionCommand>
{
    public GrantRolePermissionCommandValidator()
    {
        RuleFor(x => x.RoleId)
            .GreaterThan(0);

        RuleFor(x => x.PermissionId)
            .GreaterThan(0);
    }
}