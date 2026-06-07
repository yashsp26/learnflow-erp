using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Employees.Commands.UpdateMyEmployeeProfile
{
    public class UpdateMyEmployeeProfileCommandHandler
        : IRequestHandler<UpdateMyEmployeeProfileCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public UpdateMyEmployeeProfileCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(
            UpdateMyEmployeeProfileCommand request,
            CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(
                    x => x.UserId == _currentUser.UserId,
                    cancellationToken);

            if (employee == null)
                throw new NotFoundException("Employee profile not found");

            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.Department = request.Department;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}