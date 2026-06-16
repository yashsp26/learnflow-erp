using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Courses.Commands.DeleteCourse;

namespace LearnFlowERP.Application.Tests.UnitTests.Courses.Validators;

public class DeleteCourseCommandValidatorTests
{
    private readonly DeleteCourseCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_CourseId_Invalid()
    {
        var result =
            _validator.TestValidate(
                new DeleteCourseCommand(0));

        result.ShouldHaveValidationErrorFor(
            x => x.CourseId);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var result =
            _validator.TestValidate(
                new DeleteCourseCommand(1));

        result.ShouldNotHaveAnyValidationErrors();
    }
}