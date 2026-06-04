using LearnFlowERP.Domain.Entities;
using MediatR;

namespace LearnFlowERP.Application.Features.StudentAttendances.Queries.GetCourseAttendance
{
    public record GetCourseAttendanceQuery(long CourseId)
        : IRequest<List<StudentAttendance>>;
}