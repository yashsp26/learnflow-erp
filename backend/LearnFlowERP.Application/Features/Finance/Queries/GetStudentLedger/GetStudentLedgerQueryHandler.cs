using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Finance.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Finance.Queries.GetStudentLedger
{
    public class GetStudentLedgerQueryHandler
        : IRequestHandler<
            GetStudentLedgerQuery,
            List<StudentLedgerEntryDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetStudentLedgerQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentLedgerEntryDto>> Handle(
            GetStudentLedgerQuery request,
            CancellationToken cancellationToken)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(
                    x => x.StudentId == request.StudentId,
                    cancellationToken);

            if (student == null)
                throw new NotFoundException("Student not found");

            var entries = new List<StudentLedgerEntryDto>();

            var fees = await _context.Fees
                .Where(x => x.StudentId == request.StudentId)
                .ToListAsync(cancellationToken);

            foreach (var fee in fees)
            {
                entries.Add(new StudentLedgerEntryDto
                {
                    Date = fee.CreatedAt,
                    Type = "Fee",
                    Description = fee.FeeType.ToString(),
                    Debit = fee.TotalAmount
                });
            }

            var scholarships =
                await _context.StudentScholarships
                    .Where(x => x.StudentId == request.StudentId)
                    .ToListAsync(cancellationToken);

            foreach (var scholarship in scholarships)
            {
                entries.Add(new StudentLedgerEntryDto
                {
                    Date = scholarship.CreatedAt,
                    Type = "Scholarship",
                    Description = scholarship.ScholarshipName,
                    Credit = scholarship.Amount
                });
            }

            var payments =
                await _context.Payments
                    .Include(x => x.Fee)
                    .Where(x =>
                        x.Fee.StudentId ==
                        request.StudentId)
                    .ToListAsync(cancellationToken);

            foreach (var payment in payments)
            {
                entries.Add(new StudentLedgerEntryDto
                {
                    Date = payment.PaymentDate,
                    Type = "Payment",
                    Description = payment.ReceiptNumber,
                    Credit = payment.AmountPaid
                });
            }

            var refunds =
                await _context.Refunds
                    .Include(x => x.Payment)
                        .ThenInclude(x => x.Fee)
                    .Where(x =>
                        x.Payment.Fee.StudentId ==
                        request.StudentId)
                    .ToListAsync(cancellationToken);

            foreach (var refund in refunds)
            {
                entries.Add(new StudentLedgerEntryDto
                {
                    Date = refund.CreatedAt,
                    Type = "Refund",
                    Description = refund.Reason,
                    Debit = refund.Amount
                });
            }

            entries = entries
                .OrderBy(x => x.Date)
                .ToList();

            decimal runningBalance = 0;

            foreach (var entry in entries)
            {
                runningBalance += entry.Debit;
                runningBalance -= entry.Credit;

                entry.Balance = runningBalance;
            }

            return entries;
        }
    }
}