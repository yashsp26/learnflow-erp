using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Courses.Queries.GetCourseById;

namespace LearnFlowERP.Application.Tests.UnitTests.Courses.Validators;

public class GetCourseByIdQueryValidatorTests
{
    private readonly GetCourseByIdQueryValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_CourseId_Invalid()
    {
        var result =
            _validator.TestValidate(
                new GetCourseByIdQuery(0));

        result.ShouldHaveValidationErrorFor(
            x => x.CourseId);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var result =
            _validator.TestValidate(
                new GetCourseByIdQuery(1));

        result.ShouldNotHaveAnyValidationErrors();
    }
}