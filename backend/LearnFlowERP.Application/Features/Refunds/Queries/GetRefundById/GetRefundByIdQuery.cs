using LearnFlowERP.Application.Features.Refunds.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Refunds.Queries.GetRefundById
{
    public record GetRefundByIdQuery(long RefundId)
        : IRequest<RefundDto>;
}