using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Finance.DTOs;
using LearnFlowERP.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Finance.Queries.GetMonthlyCollection
{
    public class GetMonthlyCollectionQueryHandler
        : IRequestHandler<
            GetMonthlyCollectionQuery,
            List<MonthlyCollectionDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetMonthlyCollectionQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MonthlyCollectionDto>> Handle(
            GetMonthlyCollectionQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Payments
                .Where(x => x.Status == PaymentStatus.Success)
                .GroupBy(x => new
                {
                    x.PaymentDate.Year,
                    x.PaymentDate.Month
                })
                .Select(g => new MonthlyCollectionDto
                {
                    Year = g.Key.Year,

                    Month = g.Key.Month,

                    Amount = g.Sum(x => x.AmountPaid)
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync(cancellationToken);
        }
    }
}