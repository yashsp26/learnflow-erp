using LearnFlowERP.Application.Features.Finance.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Finance.Queries.GetCollectionSummary
{
    public record GetCollectionSummaryQuery()
        : IRequest<CollectionSummaryDto>;
}