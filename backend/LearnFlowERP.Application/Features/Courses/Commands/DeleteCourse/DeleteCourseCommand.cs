using MediatR;

namespace LearnFlowERP.Application.Features.Courses.Commands.DeleteCourse
{
    public record DeleteCourseCommand(long CourseId)
        : IRequest<Unit>;
}