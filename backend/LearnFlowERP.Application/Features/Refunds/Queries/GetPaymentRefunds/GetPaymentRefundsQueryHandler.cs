using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Refunds.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Refunds.Queries.GetPaymentRefunds
{
    public class GetPaymentRefundsQueryHandler
        : IRequestHandler<GetPaymentRefundsQuery, List<RefundDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetPaymentRefundsQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RefundDto>> Handle(
            GetPaymentRefundsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Refunds
                .Where(x => x.PaymentId == request.PaymentId)
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