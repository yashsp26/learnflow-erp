using LearnFlowERP.Application.Features.Courses.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Courses.Queries.GetCourseById
{
    public record GetCourseByIdQuery(long CourseId)
        : IRequest<CourseDto>;
}