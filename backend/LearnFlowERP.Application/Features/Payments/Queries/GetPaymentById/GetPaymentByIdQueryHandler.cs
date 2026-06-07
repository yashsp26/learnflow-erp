using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Payments.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Payments.Queries.GetPaymentById
{
    public class GetPaymentByIdQueryHandler
        : IRequestHandler<GetPaymentByIdQuery, PaymentDto>
    {
        private readonly IApplicationDbContext _context;

        public GetPaymentByIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaymentDto> Handle(
            GetPaymentByIdQuery request,
            CancellationToken cancellationToken)
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(
                    x => x.PaymentId == request.PaymentId,
                    cancellationToken);

            if (payment == null)
                throw new NotFoundException("Payment not found");

            return new PaymentDto
            {
                PaymentId = payment.PaymentId,
                FeeId = payment.FeeId,
                AmountPaid = payment.AmountPaid,
                PaymentMethod = payment.PaymentMethod,
                Status = payment.Status,
                ReceiptNumber = payment.ReceiptNumber,
                TransactionRef = payment.TransactionRef,
                PaymentDate = payment.PaymentDate
            };
        }
    }
}