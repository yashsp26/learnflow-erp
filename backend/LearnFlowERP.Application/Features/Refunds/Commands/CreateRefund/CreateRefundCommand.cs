using MediatR;

namespace LearnFlowERP.Application.Features.Refunds.Commands.CreateRefund
{
    public record CreateRefundCommand(
        long PaymentId,
        decimal Amount,
        string Reason
    ) : IRequest<long>;
}