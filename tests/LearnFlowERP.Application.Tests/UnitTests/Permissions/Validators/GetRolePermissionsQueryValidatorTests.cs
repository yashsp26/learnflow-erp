using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Permissions.Queries.GetRolePermissions;

namespace LearnFlowERP.Application.Tests.UnitTests.Permissions.Validators;

public class GetRolePermissionsQueryValidatorTests
{
    private readonly GetRolePermissionsQueryValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_RoleId_Invalid()
    {
        var result =
            _validator.TestValidate(
                new GetRolePermissionsQuery(0));

        result.ShouldHaveValidationErrorFor(
            x => x.RoleId);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var result =
            _validator.TestValidate(
                new GetRolePermissionsQuery(1));

        result.ShouldNotHaveAnyValidationErrors();
    }
}