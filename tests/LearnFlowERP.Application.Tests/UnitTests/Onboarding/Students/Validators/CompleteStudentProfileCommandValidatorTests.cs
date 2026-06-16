using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Onboarding.Students.Commands.CompleteStudentProfile;

namespace LearnFlowERP.Application.Tests.UnitTests.Onboarding.Students.Validators;

public class CompleteStudentProfileCommandValidatorTests
{
    private readonly CompleteStudentProfileCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_FirstName_Empty()
    {
        var command = new CompleteStudentProfileCommand
        {
            FirstName = "",
            LastName = "Patil",
            Dob = DateTime.Today.AddYears(-20)
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Should_Fail_When_LastName_Empty()
    {
        var command = new CompleteStudentProfileCommand
        {
            FirstName = "Amit",
            LastName = "",
            Dob = DateTime.Today.AddYears(-20)
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Fact]
    public void Should_Fail_When_Dob_In_Future()
    {
        var command = new CompleteStudentProfileCommand
        {
            FirstName = "Amit",
            LastName = "Patil",
            Dob = DateTime.Today.AddDays(1)
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Dob);
    }

    [Fact]
    public void Should_Fail_When_Dob_Too_Old()
    {
        var command = new CompleteStudentProfileCommand
        {
            FirstName = "Amit",
            LastName = "Patil",
            Dob = DateTime.Today.AddYears(-120)
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Dob);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var command = new CompleteStudentProfileCommand
        {
            FirstName = "Amit",
            LastName = "Patil",
            Dob = DateTime.Today.AddYears(-20)
        };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}