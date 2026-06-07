using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using LearnFlowERP.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Onboarding.Commands.Employees.CompleteEmployeeProfile
{
    public class CompleteEmployeeProfileCommandHandler
        : IRequestHandler<CompleteEmployeeProfileCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CompleteEmployeeProfileCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(
            CompleteEmployeeProfileCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId
                ?? throw new UnauthorizedAccessException();

            var tenantId = _currentUser.TenantId
                ?? throw new UnauthorizedAccessException();

            var user = await _context.Users
                .FirstOrDefaultAsync(
                    x => x.UserId == userId,
                    cancellationToken);

            if (user == null)
                throw new NotFoundException("User not found");

            // 🔥 Ensure correct user type
            if (user.UserType != UserType.Employee)
                throw new InvalidOperationException("Invalid user type");

            // 🔥 Prevent duplicate profile creation
            var exists = await _context.Employees
                .AnyAsync(
                    x => x.UserId == userId,
                    cancellationToken);

            if (exists)
                throw new DataAlreadyExistsException("Profile already completed");

            var currentYear = DateTime.Now.Year;

            var lastEmployee = await _context.Employees
                .OrderByDescending(x => x.EmployeeId)
                .FirstOrDefaultAsync(cancellationToken);

            var nextNumber = (lastEmployee?.EmployeeId ?? 0) + 1;

            var empCode = $"EMP-{currentYear}-{nextNumber:D4}";

            var employee = new Employee
            {
                UserId = user.UserId,
                TenantId = tenantId,
                EmpCode = empCode,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Department = request.Department,

                CreatedBy = userId
            };

            _context.Employees.Add(employee);

            // 🔥 Mark onboarding complete
            user.ProfileCompleted = true;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}