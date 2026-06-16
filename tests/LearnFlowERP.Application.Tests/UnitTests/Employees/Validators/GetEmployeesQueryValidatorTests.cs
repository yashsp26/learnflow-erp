using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Employees.Queries.GetEmployees;

namespace LearnFlowERP.Application.Tests.UnitTests.Employees.Validators;

public class GetEmployeesQueryValidatorTests
{
    private readonly GetEmployeesQueryValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_Page_Invalid()
    {
        var result =
            _validator.TestValidate(
                new GetEmployeesQuery
                {
                    Page = 0,
                    PageSize = 10
                });

        result.ShouldHaveValidationErrorFor(
            x => x.Page);
    }

    [Fact]
    public void Should_Fail_When_PageSize_Invalid()
    {
        var result =
            _validator.TestValidate(
                new GetEmployeesQuery
                {
                    Page = 1,
                    PageSize = 0
                });

        result.ShouldHaveValidationErrorFor(
            x => x.PageSize);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var result =
            _validator.TestValidate(
                new GetEmployeesQuery
                {
                    Page = 1,
                    PageSize = 10
                });

        result.ShouldNotHaveAnyValidationErrors();
    }
}