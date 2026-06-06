using LearnFlowERP.Application.Features.Fees.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Fees.Queries.GetFees
{
    public record GetFeesQuery()
        : IRequest<List<FeeDto>>;
}