using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Finance.DTOs;
using LearnFlowERP.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Finance.Queries.GetFinanceDashboard
{
    public class GetFinanceDashboardQueryHandler
        : IRequestHandler<GetFinanceDashboardQuery, FinanceDashboardDto>
    {
        private readonly IApplicationDbContext _context;

        public GetFinanceDashboardQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FinanceDashboardDto> Handle(
            GetFinanceDashboardQuery request,
            CancellationToken cancellationToken)
        {
            var totalFees =
                await _context.Fees
                    .SumAsync(
                        x => x.TotalAmount,
                        cancellationToken);

            var totalCollected =
                await _context.Payments
                    .Where(x => x.Status == PaymentStatus.Success)
                    .SumAsync(
                        x => x.AmountPaid,
                        cancellationToken);

            var outstanding =
                await _context.Fees
                    .SumAsync(
                        x => x.OutstandingAmount,
                        cancellationToken);

            var scholarships =
                await _context.StudentScholarships
                    .Where(x => x.IsActive)
                    .SumAsync(
                        x => x.Amount,
                        cancellationToken);

            var refunds =
                await _context.Refunds
                    .SumAsync(
                        x => x.Amount,
                        cancellationToken);

            var studentsWithDues =
                await _context.Fees
                    .CountAsync(
                        x => x.OutstandingAmount > 0,
                        cancellationToken);

            return new FinanceDashboardDto
            {
                TotalFees = totalFees,
                TotalCollected = totalCollected,
                OutstandingFees = outstanding,
                ScholarshipsGiven = scholarships,
                RefundsIssued = refunds,
                StudentsWithDues = studentsWithDues
            };
        }
    }
}