using LearnFlowERP.Application.Features.Finance.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Finance.Queries.GetFeeDefaulters
{
    public record GetFeeDefaulterQuery()
        : IRequest<List<FeeDefaulterDto>>;
}