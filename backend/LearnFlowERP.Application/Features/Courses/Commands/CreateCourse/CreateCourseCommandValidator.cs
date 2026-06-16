using FluentValidation;

namespace LearnFlowERP.Application.Features.Courses.Commands.CreateCourse;

public class CreateCourseCommandValidator
    : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
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