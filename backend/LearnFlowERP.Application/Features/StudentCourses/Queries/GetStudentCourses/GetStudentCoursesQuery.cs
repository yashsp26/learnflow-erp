using LearnFlowERP.Application.Features.StudentCourses.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.StudentCourses.Queries.GetStudentCourses
{
    public record GetStudentCoursesQuery(long StudentId)
        : IRequest<List<StudentCourseDto>>;
}