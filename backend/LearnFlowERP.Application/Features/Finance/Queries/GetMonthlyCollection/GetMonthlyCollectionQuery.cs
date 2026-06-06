using LearnFlowERP.Application.Features.Finance.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Finance.Queries.GetMonthlyCollection
{
    public record GetMonthlyCollectionQuery()
        : IRequest<List<MonthlyCollectionDto>>;
}