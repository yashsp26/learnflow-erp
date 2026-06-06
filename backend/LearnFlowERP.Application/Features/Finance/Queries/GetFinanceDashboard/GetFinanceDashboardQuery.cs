using LearnFlowERP.Application.Features.Finance.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Finance.Queries.GetFinanceDashboard
{
    public record GetFinanceDashboardQuery()
        : IRequest<FinanceDashboardDto>;
}