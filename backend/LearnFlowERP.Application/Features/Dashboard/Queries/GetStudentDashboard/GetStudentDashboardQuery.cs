using LearnFlowERP.Application.Features.Dashboard.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Dashboard.Queries.GetStudentDashboard
{
    public record GetStudentDashboardQuery()
        : IRequest<StudentDashboardDto>;
}