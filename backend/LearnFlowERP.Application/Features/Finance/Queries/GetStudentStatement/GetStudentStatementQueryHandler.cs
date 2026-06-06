using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Finance.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Finance.Queries.GetStudentStatement
{
    public class GetStudentStatementQueryHandler
        : IRequestHandler<GetStudentStatementQuery, StudentStatementDto>
    {
        private readonly IApplicationDbContext _context;

        public GetStudentStatementQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StudentStatementDto> Handle(
            GetStudentStatementQuery request,
            CancellationToken cancellationToken)
        {
            var student = await _context.Students
                .Include(x => x.Fees)
                .FirstOrDefaultAsync(
                    x => x.StudentId == request.StudentId,
                    cancellationToken);

            if (student == null)
                throw new NotFoundException("Student not found");

            var history =
                new List<StudentStatementItemDto>();

            var payments = await _context.Payments
                .Include(x => x.Fee)
                .Where(x =>
                    x.Fee.StudentId == request.StudentId)
                .ToListAsync(cancellationToken);

            history.AddRange(
                payments.Select(x =>
                    new StudentStatementItemDto
                    {
                        Type = "Payment",
                        Amount = x.AmountPaid,
                        Date = x.PaymentDate
                    }));

            var scholarships =
                await _context.StudentScholarships
                    .Where(x =>
                        x.StudentId == request.StudentId)
                    .ToListAsync(cancellationToken);

            history.AddRange(
                scholarships.Select(x =>
                    new StudentStatementItemDto
                    {
                        Type = "Scholarship",
                        Amount = x.Amount,
                        Date = x.CreatedAt
                    }));

            var refunds =
                await _context.Refunds
                    .Include(x => x.Payment)
                        .ThenInclude(x => x.Fee)
                    .Where(x =>
                        x.Payment.Fee.StudentId ==
                        request.StudentId)
                    .ToListAsync(cancellationToken);

            history.AddRange(
                refunds.Select(x =>
                    new StudentStatementItemDto
                    {
                        Type = "Refund",
                        Amount = x.Amount,
                        Date = x.CreatedAt
                    }));

            return new StudentStatementDto
            {
                StudentId = student.StudentId,

                StudentName =
                    $"{student.FirstName} {student.LastName}",

                TotalFees =
                    student.Fees.Sum(x => x.TotalAmount),

                PaidAmount =
                    student.Fees.Sum(x => x.PaidAmount),

                ScholarshipAmount =
                    student.Fees.Sum(x => x.ScholarshipAmount),

                RefundAmount =
                    student.Fees.Sum(x => x.RefundAmount),

                OutstandingAmount =
                    student.Fees.Sum(x => x.OutstandingAmount),

                History =
                    history.OrderByDescending(x => x.Date)
                        .ToList()
            };
        }
    }
}