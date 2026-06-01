using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Employees.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace LearnFlowERP.Application.Features.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly IApplicationDbContext _context;
        public GetEmployeeByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeDto> Handle(
            GetEmployeeByIdQuery request,
            CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .Include(x => x.User)
                .FirstOrDefaultAsync(
                    x => x.EmployeeId == request.EmployeeId && x.IsActive);

            if (employee == null)
                throw new Exception("Employee not found");

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
