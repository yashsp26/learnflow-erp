using LearnFlowERP.Application.Features.StudentAttendances.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.StudentAttendances.Commands.MarkStudentAttendance
{
    public record MarkStudentAttendanceCommand(
        long CourseId,
        List<MarkStudentAttendanceDto> Students
    ) : IRequest<Unit>;
}