using LearnFlowERP.Application.Features.EmployeeAttendances.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.EmployeeAttendances.Queries.GetEmployeeAttendance
{
    public record GetEmployeeAttendanceQuery(
        long EmployeeId
    ) : IRequest<List<EmployeeAttendanceDto>>;
}