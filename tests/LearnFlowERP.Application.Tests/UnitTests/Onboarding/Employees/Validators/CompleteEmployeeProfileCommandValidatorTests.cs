using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Onboarding.Commands.Employees.CompleteEmployeeProfile;

namespace LearnFlowERP.Application.Tests.UnitTests.Onboarding.Employees.Validators;

public class CompleteEmployeeProfileCommandValidatorTests
{
    private readonly CompleteEmployeeProfileCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_FirstName_Empty()
    {
        var command = new CompleteEmployeeProfileCommand
        {
            FirstName = "",
            LastName = "Doe",
            Department = "IT"
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Should_Fail_When_LastName_Empty()
    {
        var command = new CompleteEmployeeProfileCommand
        {
            FirstName = "John",
            LastName = "",
            Department = "IT"
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Fact]
    public void Should_Fail_When_Department_Empty()
    {
        var command = new CompleteEmployeeProfileCommand
        {
            FirstName = "John",
            LastName = "Doe",
            Department = ""
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Department);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var command = new CompleteEmployeeProfileCommand
        {
            FirstName = "John",
            LastName = "Doe",
            Department = "IT"
        };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}