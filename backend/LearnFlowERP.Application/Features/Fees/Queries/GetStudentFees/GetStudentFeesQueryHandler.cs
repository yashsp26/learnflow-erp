using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Fees.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Fees.Queries.GetStudentFees
{
    public class GetStudentFeesQueryHandler
        : IRequestHandler<GetStudentFeesQuery, List<FeeDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetStudentFeesQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FeeDto>> Handle(
            GetStudentFeesQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Fees
                .Where(x => x.StudentId == request.StudentId)
                .Select(fee => new FeeDto
                {
                    FeeId = fee.FeeId,
                    StudentId = fee.StudentId,
                    TotalAmount = fee.TotalAmount,
                    PaidAmount = fee.PaidAmount,
                    PendingAmount = fee.TotalAmount - fee.PaidAmount,
                    FeeType = fee.FeeType,
                    AcademicYear = fee.AcademicYear,
                    Status = fee.Status,
                    DueDate = fee.DueDate
                })
                .ToListAsync(cancellationToken);
        }
    }
}