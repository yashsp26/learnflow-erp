using LearnFlowERP.Application.Features.TeacherCourses.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.TeacherCourses.Queries.GetCourseTeachers
{
    public record GetCourseTeachersQuery(long CourseId)
        : IRequest<List<TeacherCourseDto>>;
}