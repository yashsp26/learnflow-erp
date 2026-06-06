using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Refunds.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Refunds.Queries.GetRefunds
{
    public class GetRefundsQueryHandler
        : IRequestHandler<GetRefundsQuery, List<RefundDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetRefundsQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RefundDto>> Handle(
            GetRefundsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Refunds
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new RefundDto
                {
                    RefundId = x.RefundId,
                    PaymentId = x.PaymentId,
                    Amount = x.Amount,
                    Reason = x.Reason,
                    ApprovedBy = x.ApprovedBy,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}