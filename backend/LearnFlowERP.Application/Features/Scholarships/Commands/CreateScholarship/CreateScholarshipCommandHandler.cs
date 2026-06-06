using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Scholarships.Commands.CreateScholarship
{
    public class CreateScholarshipCommandHandler
        : IRequestHandler<CreateScholarshipCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CreateScholarshipCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<long> Handle(
            CreateScholarshipCommand request,
            CancellationToken cancellationToken)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(
                    x => x.StudentId == request.StudentId,
                    cancellationToken);

            if (student == null)
                throw new NotFoundException("Student not found");

            var fee = await _context.Fees
                .FirstOrDefaultAsync(
                    x => x.FeeId == request.FeeId,
                    cancellationToken);

            if (fee == null)
                throw new NotFoundException("Fee not found");

            if (fee.StudentId != request.StudentId)
                throw new NotFoundException("Fee does not belong to student");

            var scholarship = new StudentScholarship
            {
                StudentId = request.StudentId,
                TenantId = _currentUser.TenantId!.Value,
                ScholarshipName = request.ScholarshipName,
                Amount = request.Amount,
                EffectiveFrom = request.EffectiveFrom,
                EffectiveTo = request.EffectiveTo,
                IsActive = true
            };

            _context.StudentScholarships.Add(scholarship);

            fee.ScholarshipAmount += request.Amount;

            fee.Recalculate();

            await _context.SaveChangesAsync(cancellationToken);

            return scholarship.StudentScholarshipId;
        }
    }
}