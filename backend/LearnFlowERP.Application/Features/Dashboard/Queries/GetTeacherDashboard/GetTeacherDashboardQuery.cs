using LearnFlowERP.Application.Features.Dashboard.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Dashboard.Queries.GetTeacherDashboard
{
    public record GetTeacherDashboardQuery()
        : IRequest<TeacherDashboardDto>;
}