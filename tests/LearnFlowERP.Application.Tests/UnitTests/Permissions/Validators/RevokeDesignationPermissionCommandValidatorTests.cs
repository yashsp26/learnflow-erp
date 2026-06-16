using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Permissions.Commands.RevokeDesignationPermission;

namespace LearnFlowERP.Application.Tests.UnitTests.Permissions.Validators;

public class RevokeDesignationPermissionCommandValidatorTests
{
    private readonly RevokeDesignationPermissionCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_DesignationId_Invalid()
    {
        var result =
            _validator.TestValidate(
                new RevokeDesignationPermissionCommand(
                    0,
                    1));

        result.ShouldHaveValidationErrorFor(
            x => x.DesignationId);
    }

    [Fact]
    public void Should_Fail_When_PermissionId_Invalid()
    {
        var result =
            _validator.TestValidate(
                new RevokeDesignationPermissionCommand(
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
                new RevokeDesignationPermissionCommand(
                    1,
                    1));

        result.ShouldNotHaveAnyValidationErrors();
    }
}