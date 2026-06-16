using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Employees.Commands.DeleteEmployee;

namespace LearnFlowERP.Application.Tests.UnitTests.Employees.Validators;

public class DeleteEmployeeCommandValidatorTests
{
    private readonly DeleteEmployeeCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_EmployeeId_Invalid()
    {
        var result =
            _validator.TestValidate(
                new DeleteEmployeeCommand(0));

        result.ShouldHaveValidationErrorFor(
            x => x.EmployeeId);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var result =
            _validator.TestValidate(
                new DeleteEmployeeCommand(1));

        result.ShouldNotHaveAnyValidationErrors();
    }
}