using FluentValidation;

namespace LearnFlowERP.Application.Features.Courses.Queries.GetCourseById;

public class GetCourseByIdQueryValidator
    : AbstractValidator<GetCourseByIdQuery>
{
    public GetCourseByIdQueryValidator()
    {
        RuleFor(x => x.CourseId)
            .GreaterThan(0);
    }
}