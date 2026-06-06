using LearnFlowERP.Application.Features.Refunds.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Refunds.Queries.GetPaymentRefunds
{
    public record GetPaymentRefundsQuery(long PaymentId)
        : IRequest<List<RefundDto>>;
}