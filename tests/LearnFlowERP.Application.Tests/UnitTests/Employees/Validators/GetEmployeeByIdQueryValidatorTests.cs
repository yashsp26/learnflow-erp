using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Employees.Queries.GetEmployeeById;

namespace LearnFlowERP.Application.Tests.UnitTests.Employees.Validators;

public class GetEmployeeByIdQueryValidatorTests
{
    private readonly GetEmployeeByIdQueryValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_EmployeeId_Invalid()
    {
        var result =
            _validator.TestValidate(
                new GetEmployeeByIdQuery(0));

        result.ShouldHaveValidationErrorFor(
            x => x.EmployeeId);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var result =
            _validator.TestValidate(
                new GetEmployeeByIdQuery(1));

        result.ShouldNotHaveAnyValidationErrors();
    }
}