using LearnFlowERP.Application.Features.TeacherCourses.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.TeacherCourses.Queries.GetTeacherCourses
{
    public record GetTeacherCoursesQuery(long EmployeeId)
        : IRequest<List<TeacherCourseDto>>;
}