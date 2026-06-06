using LearnFlowERP.Application.Features.Finance.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Finance.Queries.GetOutstandingFees
{
    public record GetOutstandingFeesQuery()
        : IRequest<List<OutstandingFeeDto>>;
}