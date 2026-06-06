using LearnFlowERP.Application.Features.Payments.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Payments.Queries.GetPaymentById
{
    public record GetPaymentByIdQuery(long PaymentId)
        : IRequest<PaymentDto>;
}