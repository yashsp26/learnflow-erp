using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using LearnFlowERP.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandHandler
        : IRequestHandler<CreatePaymentCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CreatePaymentCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<long> Handle(
            CreatePaymentCommand request,
            CancellationToken cancellationToken)
        {
            var fee = await _context.Fees
                .FirstOrDefaultAsync(
                    x => x.FeeId == request.FeeId,
                    cancellationToken);

            if (fee == null)
                throw new Exception("Fee not found");

            var pendingAmount =
                fee.OutstandingAmount;

            if (request.AmountPaid > pendingAmount)
                throw new Exception(
                    "Payment exceeds pending amount");

            var receiptNumber =
                $"RCPT-{DateTime.Now.Year}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";

            var payment = new Payment
            {
                FeeId = fee.FeeId,
                TenantId = fee.TenantId,

                AmountPaid = request.AmountPaid,

                PaymentMethod = request.PaymentMethod,

                Status = PaymentStatus.Success,

                ReceiptNumber = receiptNumber,

                TransactionRef = request.TransactionRef,

                Remarks = request.Remarks,

                CreatedBy = _currentUser.UserId
            };

            _context.Payments.Add(payment);

            fee.PaidAmount += request.AmountPaid;

            fee.Recalculate();

            await _context.SaveChangesAsync(cancellationToken);

            return payment.PaymentId;
        }
    }
}