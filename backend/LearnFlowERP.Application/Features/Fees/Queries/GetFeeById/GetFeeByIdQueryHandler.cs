using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Fees.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Fees.Queries.GetFeeById
{
    public class GetFeeByIdQueryHandler
        : IRequestHandler<GetFeeByIdQuery, FeeDto>
    {
        private readonly IApplicationDbContext _context;

        public GetFeeByIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FeeDto> Handle(
            GetFeeByIdQuery request,
            CancellationToken cancellationToken)
        {
            var fee = await _context.Fees
                .FirstOrDefaultAsync(
                    x => x.FeeId == request.FeeId,
                    cancellationToken);

            if (fee == null)
                throw new Exception("Fee not found");

            return new FeeDto
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
            };
        }
    }
}