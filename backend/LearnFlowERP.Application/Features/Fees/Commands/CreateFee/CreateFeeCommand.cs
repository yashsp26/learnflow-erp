using MediatR;

namespace LearnFlowERP.Application.Features.Fees.Commands.CreateFee
{
    public record CreateFeeCommand(
        long StudentId,
        decimal TotalAmount,
        DateTime DueDate,
        string FeeType,
        string AcademicYear
    ) : IRequest<long>;
}