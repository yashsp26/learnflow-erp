using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using LearnFlowERP.Domain.Enums;
using MediatR;

namespace LearnFlowERP.Application.Features.Fees.Commands.CreateFee
{
    public class CreateFeeCommandHandler
        : IRequestHandler<CreateFeeCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CreateFeeCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<long> Handle(
            CreateFeeCommand request,
            CancellationToken cancellationToken)
        {
            var fee = new Fee
            {
                StudentId = request.StudentId,
                TenantId = _currentUser.TenantId!.Value,
                TotalAmount = request.TotalAmount,
                PaidAmount = 0,
                ScholarshipAmount = 0,
                RefundAmount = 0,
                DueDate = request.DueDate,
                FeeType = request.FeeType,
                AcademicYear = request.AcademicYear,
                CreatedBy = _currentUser.UserId,
                LateFeeAmount = 0,
                IsLateFeeApplied = false
            };

            fee.Recalculate();

            _context.Fees.Add(fee);

            await _context.SaveChangesAsync(cancellationToken);

            return fee.FeeId;
        }
    }
}