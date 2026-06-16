using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Permissions.Commands.GrantDesignationPermission;

namespace LearnFlowERP.Application.Tests.UnitTests.Permissions.Validators;

public class GrantDesignationPermissionCommandValidatorTests
{
    private readonly GrantDesignationPermissionCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_DesignationId_Invalid()
    {
        var command =
            new GrantDesignationPermissionCommand(
                0,
                1);

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            x => x.DesignationId);
    }

    [Fact]
    public void Should_Fail_When_PermissionId_Invalid()
    {
        var command =
            new GrantDesignationPermissionCommand(
                1,
                0);

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            x => x.PermissionId);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var command =
            new GrantDesignationPermissionCommand(
                1,
                1);

        var result =
            _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}