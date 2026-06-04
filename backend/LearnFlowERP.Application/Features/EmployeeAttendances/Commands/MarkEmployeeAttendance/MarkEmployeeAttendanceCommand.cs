using LearnFlowERP.Domain.Enums;
using MediatR;

namespace LearnFlowERP.Application.Features.EmployeeAttendances.Commands.MarkEmployeeAttendance
{
    public record MarkEmployeeAttendanceCommand(
        long EmployeeId,
        AttendanceStatus Status
    ) : IRequest<Unit>;
}