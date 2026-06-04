using LearnFlowERP.Application.Features.StudentAttendances.DTOs;
using LearnFlowERP.Application.Features.StudentAttendances.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.StudentAttendances.Queries.GetStudentAttendance
{
    public record GetStudentAttendanceQuery(long StudentId)
        : IRequest<List<StudentAttendanceDto>>;
}