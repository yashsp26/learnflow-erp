using LearnFlowERP.Application.Features.Dashboard.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Dashboard.Queries.GetAdminDashboard
{
    public record GetAdminDashboardQuery()
        : IRequest<AdminDashboardDto>;
}