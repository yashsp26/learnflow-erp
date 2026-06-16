using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Permissions.Commands.GrantRolePermission;

namespace LearnFlowERP.Application.Tests.UnitTests.Permissions.Validators;

public class GrantRolePermissionCommandValidatorTests
{
    private readonly GrantRolePermissionCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_RoleId_Invalid()
    {
        var result =
            _validator.TestValidate(
                new GrantRolePermissionCommand(
                    0,
                    1));

        result.ShouldHaveValidationErrorFor(
            x => x.RoleId);
    }

    [Fact]
    public void Should_Fail_When_PermissionId_Invalid()
    {
        var result =
            _validator.TestValidate(
                new GrantRolePermissionCommand(
                    1,
                    0));

        result.ShouldHaveValidationErrorFor(
            x => x.PermissionId);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var result =
            _validator.TestValidate(
                new GrantRolePermissionCommand(
                    1,
                    1));

        result.ShouldNotHaveAnyValidationErrors();
    }
}