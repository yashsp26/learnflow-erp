using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Employees.Commands.UpdateMyEmployeeProfile;

namespace LearnFlowERP.Application.Tests.UnitTests.Employees.Validators;

public class UpdateMyEmployeeProfileCommandValidatorTests
{
    private readonly UpdateMyEmployeeProfileCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_FirstName_Empty()
    {
        var command =
            new UpdateMyEmployeeProfileCommand
            {
                FirstName = "",
                LastName = "Doe",
                Department = "IT"
            };

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            x => x.FirstName);
    }

    [Fact]
    public void Should_Fail_When_LastName_Empty()
    {
        var command =
            new UpdateMyEmployeeProfileCommand
            {
                FirstName = "John",
                LastName = "",
                Department = "IT"
            };

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            x => x.LastName);
    }

    [Fact]
    public void Should_Fail_When_Department_Empty()
    {
        var command =
            new UpdateMyEmployeeProfileCommand
            {
                FirstName = "John",
                LastName = "Doe",
                Department = ""
            };

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            x => x.Department);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var command =
            new UpdateMyEmployeeProfileCommand
            {
                FirstName = "John",
                LastName = "Doe",
                Department = "IT"
            };

        var result =
            _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}