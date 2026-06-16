using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Permissions.Queries.GetDesignationPermissions;

namespace LearnFlowERP.Application.Tests.UnitTests.Permissions.Validators;

public class GetDesignationPermissionsQueryValidatorTests
{
    private readonly GetDesignationPermissionsQueryValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_DesignationId_Invalid()
    {
        var result =
            _validator.TestValidate(
                new GetDesignationPermissionsQuery(0));

        result.ShouldHaveValidationErrorFor(
            x => x.DesignationId);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var result =
            _validator.TestValidate(
                new GetDesignationPermissionsQuery(1));

        result.ShouldNotHaveAnyValidationErrors();
    }
}