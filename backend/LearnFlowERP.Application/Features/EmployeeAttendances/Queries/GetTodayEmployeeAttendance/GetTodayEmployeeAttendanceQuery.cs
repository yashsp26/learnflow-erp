using LearnFlowERP.Application.Features.EmployeeAttendances.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.EmployeeAttendances.Queries.GetTodayEmployeeAttendance
{
    public record GetTodayEmployeeAttendanceQuery()
        : IRequest<List<EmployeeAttendanceDto>>;
}