using FluentValidation;

namespace LearnFlowERP.Application.Features.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandValidator
    : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseCommandValidator()
    {
        RuleFor(x => x.CourseId)
            .GreaterThan(0);

        RuleFor(x => x.CourseCode)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.CourseName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Credits)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(10);
    }
}