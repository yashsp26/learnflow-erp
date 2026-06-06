using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Finance.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Finance.Queries.GetFeeDefaulters
{
    public class GetFeeDefaultersQueryHandler
        : IRequestHandler<
            GetFeeDefaulterQuery,
            List<FeeDefaulterDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetFeeDefaultersQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FeeDefaulterDto>> Handle(
            GetFeeDefaulterQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Fees
                .Include(x => x.Student)
                .Where(x =>
                    x.OutstandingAmount > 0 &&
                    x.DueDate != null &&
                    x.DueDate < DateTime.Today)
                .OrderByDescending(x => x.OutstandingAmount)
                .Select(x => new FeeDefaulterDto
                {
                    StudentId = x.StudentId,

                    StudentName =
                        (x.Student.FirstName ?? "") +
                        " " +
                        (x.Student.LastName ?? ""),

                    OutstandingAmount =
                        x.OutstandingAmount,

                    DueDate = x.DueDate
                })
                .ToListAsync(cancellationToken);
        }
    }
}