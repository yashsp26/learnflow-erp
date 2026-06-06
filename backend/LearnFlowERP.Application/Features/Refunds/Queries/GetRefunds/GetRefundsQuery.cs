using LearnFlowERP.Application.Features.Refunds.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Refunds.Queries.GetRefunds
{
    public record GetRefundsQuery()
        : IRequest<List<RefundDto>>;
}