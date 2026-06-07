using LearnFlowERP.Application.Features.Payments.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Payments.Queries.GetReceiptByPaymentId
{
    public record GetReceiptByPaymentIdQuery(long PaymentId)
        : IRequest<ReceiptDto>;
}