using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Refunds.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Refunds.Queries.GetRefundById
{
    public class GetRefundByIdQueryHandler
        : IRequestHandler<GetRefundByIdQuery, RefundDto>
    {
        private readonly IApplicationDbContext _context;

        public GetRefundByIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RefundDto> Handle(
            GetRefundByIdQuery request,
            CancellationToken cancellationToken)
        {
            var refund = await _context.Refunds
                .FirstOrDefaultAsync(
                    x => x.RefundId == request.RefundId,
                    cancellationToken);

            if (refund == null)
                throw new NotFoundException("Refund not found");

            return new RefundDto
            {
                RefundId = refund.RefundId,
                PaymentId = refund.PaymentId,
                Amount = refund.Amount,
                Reason = refund.Reason,
                ApprovedBy = refund.ApprovedBy,
                CreatedAt = refund.CreatedAt
            };
        }
    }
}