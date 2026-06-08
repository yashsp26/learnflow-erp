using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using LearnFlowERP.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Refunds.Commands.CreateRefund
{
    public class CreateRefundCommandHandler
        : IRequestHandler<CreateRefundCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly INotificationService _notificationService;
        public CreateRefundCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser,
            INotificationService notificationService)
        {
            _context = context;
            _currentUser = currentUser;
            _notificationService = notificationService;
        }

        public async Task<long> Handle(
            CreateRefundCommand request,
            CancellationToken cancellationToken)
        {
            var payment = await _context.Payments
                .Include(x => x.Fee)
                .FirstOrDefaultAsync(
                    x => x.PaymentId == request.PaymentId,
                    cancellationToken);

            if (payment == null)
                throw new NotFoundException("Payment not found");

            var refundedAlready =
                await _context.Refunds
                    .Where(x => x.PaymentId == payment.PaymentId)
                    .SumAsync(x => x.Amount, cancellationToken);

            var remainingRefundable =
                payment.AmountPaid - refundedAlready;

            if (request.Amount > remainingRefundable)
                throw new InvalidOperationException(
                    "Refund exceeds payment amount");

            var refund = new Refund
            {
                PaymentId = payment.PaymentId,
                TenantId = payment.TenantId,
                Amount = request.Amount,
                Reason = request.Reason,
                ApprovedBy = _currentUser.UserId
            };

            _context.Refunds.Add(refund);

            payment.Fee.PaidAmount -= request.Amount;

            if (payment.Fee.PaidAmount < 0)
            {
                payment.Fee.PaidAmount = 0;
            }

            payment.Fee.RefundAmount += request.Amount;

            payment.Fee.Recalculate();

            if (remainingRefundable == request.Amount)
            {
                payment.Status = PaymentStatus.Refunded;
            }

            _context.PaymentAudits.Add(
                new PaymentAudit
                {
                    PaymentId = payment.PaymentId,
                    OldStatus = PaymentStatus.Success.ToString(),
                    NewStatus = payment.Status.ToString(),
                    ChangedBy = _currentUser.UserId
                });

            await _context.SaveChangesAsync(cancellationToken);

            await _notificationService.SendToStudentAsync(
                payment.Fee.StudentId,
                "Refund Approved",
                $"Refund of ₹{request.Amount:N2} has been approved.",
                NotificationModule.Finance,
                refund.RefundId);

            return refund.RefundId;
        }
    }
}