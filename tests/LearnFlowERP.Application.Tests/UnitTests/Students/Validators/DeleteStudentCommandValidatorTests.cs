using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Students.Commands.DeleteStudent;

namespace LearnFlowERP.Application.Tests.UnitTests.Students.Validators;

public class DeleteStudentCommandValidatorTests
{
    private readonly DeleteStudentCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_StudentId_Is_Zero()
    {
        var command =
            new DeleteStudentCommand(0);

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            x => x.StudentId);
    }

    [Fact]
    public void Should_Fail_When_StudentId_Is_Negative()
    {
        var command =
            new DeleteStudentCommand(-1);

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            x => x.StudentId);
    }

    [Fact]
    public void Should_Pass_When_StudentId_Is_Valid()
    {
        var command =
            new DeleteStudentCommand(1);

        var result =
            _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}