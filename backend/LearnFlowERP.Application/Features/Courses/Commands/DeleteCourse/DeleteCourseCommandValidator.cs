using FluentValidation;

namespace LearnFlowERP.Application.Features.Courses.Commands.DeleteCourse;

public class DeleteCourseCommandValidator
    : AbstractValidator<DeleteCourseCommand>
{
    public DeleteCourseCommandValidator()
    {
        RuleFor(x => x.CourseId)
            .GreaterThan(0);
    }
}