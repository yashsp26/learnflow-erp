using LearnFlowERP.Domain.Enums;
using MediatR;

namespace LearnFlowERP.Application.Features.Payments.Commands.CreatePayment
{
    public record CreatePaymentCommand(
        long FeeId,
        decimal AmountPaid,
        PaymentMethod PaymentMethod,
        string? TransactionRef,
        string? Remarks
    ) : IRequest<long>;
}