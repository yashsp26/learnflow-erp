using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Students.Commands.UpdateMyStudentProfile;

namespace LearnFlowERP.Application.Tests.UnitTests.Students.Validators;

public class UpdateMyStudentProfileCommandValidatorTests
{
    private readonly UpdateMyStudentProfileCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_FirstName_Empty()
    {
        var command =
            new UpdateMyStudentProfileCommand
            {
                FirstName = "",
                LastName = "Smith",
                Dob = DateTime.Today.AddYears(-20)
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
            new UpdateMyStudentProfileCommand
            {
                FirstName = "John",
                LastName = "",
                Dob = DateTime.Today.AddYears(-20)
            };

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            x => x.LastName);
    }

    [Fact]
    public void Should_Fail_When_Dob_Is_Future()
    {
        var command =
            new UpdateMyStudentProfileCommand
            {
                FirstName = "John",
                LastName = "Smith",
                Dob = DateTime.Today.AddDays(1)
            };

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            x => x.Dob);
    }

    [Fact]
    public void Should_Pass_When_Request_Is_Valid()
    {
        var command =
            new UpdateMyStudentProfileCommand
            {
                FirstName = "John",
                LastName = "Smith",
                Dob = DateTime.Today.AddYears(-20)
            };

        var result =
            _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}