using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Employees.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Employees.Queries.GetMyEmployeeProfile
{
    public class GetMyEmployeeProfileQueryHandler
        : IRequestHandler<GetMyEmployeeProfileQuery, EmployeeDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetMyEmployeeProfileQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<EmployeeDto> Handle(
            GetMyEmployeeProfileQuery request,
            CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .Include(x => x.User)
                .FirstOrDefaultAsync(
                    x => x.UserId == _currentUser.UserId,
                    cancellationToken);

            if (employee == null)
                throw new Exception("Employee profile not found");

            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                UserId = employee.UserId,
                EmpCode = employee.EmpCode,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Department = employee.Department,
                Salary = employee.Salary,
                Email = employee.User!.Email,
                DocumentUrl = employee.DocumentUrl
            };
        }
    }
}