using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Finance.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Finance.Queries.GetOutstandingFees
{
    public class GetOutstandingFeesQueryHandler
        : IRequestHandler<
            GetOutstandingFeesQuery,
            List<OutstandingFeeDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetOutstandingFeesQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OutstandingFeeDto>> Handle(
            GetOutstandingFeesQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Fees
                .Include(x => x.Student)
                .Where(x => x.OutstandingAmount > 0)
                .OrderByDescending(x => x.OutstandingAmount)
                .Select(x => new OutstandingFeeDto
                {
                    StudentId = x.StudentId,

                    StudentName =
                        (x.Student.FirstName ?? "") +
                        " " +
                        (x.Student.LastName ?? ""),

                    TotalFee = x.TotalAmount,

                    Outstanding = x.OutstandingAmount,

                    DueDate = x.DueDate
                })
                .ToListAsync(cancellationToken);
        }
    }
}