using LearnFlowERP.Application.Features.StudentCourses.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.StudentCourses.Queries.GetCourseStudents
{
    public record GetCourseStudentsQuery(long CourseId)
        : IRequest<List<StudentCourseDto>>;
}