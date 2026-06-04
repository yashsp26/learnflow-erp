using LearnFlowERP.Domain.Entities;
using MediatR;

namespace LearnFlowERP.Application.Features.StudentAttendances.Queries.GetTodayAttendance
{
    public record GetTodayAttendanceQuery()
        : IRequest<List<StudentAttendance>>;
}