using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Courses.Commands.CreateCourse;
using LearnFlowERP.Application.Features.Courses.Commands.UpdateCourse;

namespace LearnFlowERP.Application.Tests.UnitTests.Courses.Validators;

public class UpdateCourseCommandValidatorTests
{
    private readonly UpdateCourseCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_CourseId_Invalid()
    {
        var result =
            _validator.TestValidate(
                new UpdateCourseCommand
                {
                    CourseId = 0
                });

        result.ShouldHaveValidationErrorFor(
            x => x.CourseId);
    }

    [Fact]
    public void Should_Pass_When_Credits_Are_Zero()
    {
        var command = new UpdateCourseCommand
        {
            CourseId = 1,
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
        var command = new UpdateCourseCommand
        {
            CourseId = 1,
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
                new UpdateCourseCommand
                {
                    CourseId = 1,
                    CourseCode = "MTH101",
                    CourseName = "Math",
                    Credits = 3
                });

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Fail_When_CourseCode_Empty()
    {
        var command = new UpdateCourseCommand
        {
            CourseId = 1,
            CourseCode = "",
            CourseName = "Programming",
            Credits = 3
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.CourseCode);
    }

    [Fact]
    public void Should_Fail_When_CourseName_Empty()
    {
        var command = new UpdateCourseCommand
        {
            CourseId = 1,
            CourseCode = "CS101",
            CourseName = "",
            Credits = 3
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.CourseName);
    }

    [Fact]
    public void Should_Fail_When_Credits_Greater_Than_10()
    {
        var command = new UpdateCourseCommand
        {
            CourseId = 1,
            CourseCode = "CS101",
            CourseName = "Programming",
            Credits = 11
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Credits);
    }
}