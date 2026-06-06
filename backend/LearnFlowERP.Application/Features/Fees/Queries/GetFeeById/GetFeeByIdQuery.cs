using LearnFlowERP.Application.Features.Fees.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Fees.Queries.GetFeeById
{
    public record GetFeeByIdQuery(long FeeId)
        : IRequest<FeeDto>;
}