using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Courses.Commands.CreateCourse;

namespace LearnFlowERP.Application.Tests.Courses.Validators;
public class CreateCourseCommandValidatorTests
{
    private readonly CreateCourseCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_Code_Empty()
    {
        var result =
            _validator.TestValidate(
                new CreateCourseCommand
                {
                    CourseCode = "",
                    CourseName = "Math",
                    Credits = 3
                });

        result.ShouldHaveValidationErrorFor(
            x => x.CourseCode);
    }

    [Fact]
    public void Should_Pass_When_Credits_Are_Zero()
    {
        var command = new CreateCourseCommand
        {
            CourseCode = "ORI001",
            CourseName = "Orientation",
            Credits = 0
        };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Fail_When_Credits_Are_Negative()
    {
        var command = new CreateCourseCommand
        {
            CourseCode = "CS101",
            CourseName = "Programming",
            Credits = -1
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Credits);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var result =
            _validator.TestValidate(
                new CreateCourseCommand
                {
                    CourseCode = "MTH101",
                    CourseName = "Math",
                    Credits = 3
                });

        result.ShouldNotHaveAnyValidationErrors();
    }
}