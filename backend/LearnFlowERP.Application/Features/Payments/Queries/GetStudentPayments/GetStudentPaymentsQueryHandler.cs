using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Payments.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Payments.Queries.GetStudentPayments
{
    public class GetStudentPaymentsQueryHandler
        : IRequestHandler<GetStudentPaymentsQuery, List<PaymentDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetStudentPaymentsQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PaymentDto>> Handle(
            GetStudentPaymentsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Payments
                .Include(x => x.Fee)
                .Where(x => x.Fee.StudentId == request.StudentId)
                .OrderByDescending(x => x.PaymentDate)
                .Select(payment => new PaymentDto
                {
                    PaymentId = payment.PaymentId,
                    FeeId = payment.FeeId,
                    AmountPaid = payment.AmountPaid,
                    PaymentMethod = payment.PaymentMethod,
                    Status = payment.Status,
                    ReceiptNumber = payment.ReceiptNumber,
                    TransactionRef = payment.TransactionRef,
                    PaymentDate = payment.PaymentDate
                })
                .ToListAsync(cancellationToken);
        }
    }
}