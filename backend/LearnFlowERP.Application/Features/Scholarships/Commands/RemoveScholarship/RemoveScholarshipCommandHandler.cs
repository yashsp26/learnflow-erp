using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Scholarships.Commands.RemoveScholarship
{
    public class RemoveScholarshipCommandHandler
        : IRequestHandler<RemoveScholarshipCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public RemoveScholarshipCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            RemoveScholarshipCommand request,
            CancellationToken cancellationToken)
        {
            var scholarship =
                await _context.StudentScholarships
                    .FirstOrDefaultAsync(
                        x => x.StudentScholarshipId ==
                             request.StudentScholarshipId,
                        cancellationToken);

            if (scholarship == null)
                throw new Exception("Scholarship not found");

            if (!scholarship.IsActive)
                return Unit.Value;

            var fee = await _context.Fees
                .FirstOrDefaultAsync(
                    x => x.FeeId == scholarship.FeeId,
                    cancellationToken);

            if (fee != null)
            {
                fee.ScholarshipAmount -= scholarship.Amount;

                if (fee.ScholarshipAmount < 0)
                {
                    fee.ScholarshipAmount = 0;
                }

                fee.Recalculate();
            }

            scholarship.IsActive = false;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}