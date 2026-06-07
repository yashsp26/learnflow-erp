using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Payments.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Payments.Queries.GetReceiptByPaymentId
{
    public class GetReceiptByPaymentIdQueryHandler
        : IRequestHandler<
            GetReceiptByPaymentIdQuery,
            ReceiptDto>
    {
        private readonly IApplicationDbContext _context;

        public GetReceiptByPaymentIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReceiptDto> Handle(
            GetReceiptByPaymentIdQuery request,
            CancellationToken cancellationToken)
        {
            var payment = await _context.Payments
                .Include(x => x.Fee)
                    .ThenInclude(x => x.Student)
                .FirstOrDefaultAsync(
                    x => x.PaymentId == request.PaymentId,
                    cancellationToken);

            if (payment == null)
                throw new NotFoundException("Payment not found");

            return new ReceiptDto
            {
                PaymentId = payment.PaymentId,

                ReceiptNumber = payment.ReceiptNumber,

                StudentName =
                    $"{payment.Fee.Student.FirstName} " +
                    $"{payment.Fee.Student.LastName}",

                FeeType = payment.Fee.FeeType,

                AmountPaid = payment.AmountPaid,

                PaymentMethod =
                    payment.PaymentMethod.ToString(),

                TransactionRef =
                    payment.TransactionRef,

                PaymentDate =
                    payment.PaymentDate
            };
        }
    }
}