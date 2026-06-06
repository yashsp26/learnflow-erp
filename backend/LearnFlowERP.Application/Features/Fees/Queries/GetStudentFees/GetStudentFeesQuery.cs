using LearnFlowERP.Application.Features.Fees.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Fees.Queries.GetStudentFees
{
    public record GetStudentFeesQuery(long StudentId)
        : IRequest<List<FeeDto>>;
}