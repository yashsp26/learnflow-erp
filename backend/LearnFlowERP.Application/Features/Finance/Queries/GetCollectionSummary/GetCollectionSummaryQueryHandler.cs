using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Finance.DTOs;
using LearnFlowERP.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Finance.Queries.GetCollectionSummary
{
    public class GetCollectionSummaryQueryHandler
        : IRequestHandler<GetCollectionSummaryQuery, CollectionSummaryDto>
    {
        private readonly IApplicationDbContext _context;

        public GetCollectionSummaryQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CollectionSummaryDto> Handle(
            GetCollectionSummaryQuery request,
            CancellationToken cancellationToken)
        {
            var today = DateTime.Today;

            return new CollectionSummaryDto
            {
                TodayCollection =
                    await _context.Payments
                        .Where(x =>
                            x.Status == PaymentStatus.Success &&
                            x.PaymentDate.Date == today)
                        .SumAsync(
                            x => x.AmountPaid,
                            cancellationToken),

                ThisMonthCollection =
                    await _context.Payments
                        .Where(x =>
                            x.Status == PaymentStatus.Success &&
                            x.PaymentDate.Month == today.Month &&
                            x.PaymentDate.Year == today.Year)
                        .SumAsync(
                            x => x.AmountPaid,
                            cancellationToken),

                ThisYearCollection =
                    await _context.Payments
                        .Where(x =>
                            x.Status == PaymentStatus.Success &&
                            x.PaymentDate.Year == today.Year)
                        .SumAsync(
                            x => x.AmountPaid,
                            cancellationToken)
            };
        }
    }
}