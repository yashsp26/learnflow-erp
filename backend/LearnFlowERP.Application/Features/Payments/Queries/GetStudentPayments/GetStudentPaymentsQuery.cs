using LearnFlowERP.Application.Features.Payments.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Payments.Queries.GetStudentPayments
{
    public record GetStudentPaymentsQuery(long StudentId)
        : IRequest<List<PaymentDto>>;
}